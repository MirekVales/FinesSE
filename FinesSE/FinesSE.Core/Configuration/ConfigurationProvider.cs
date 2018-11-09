using FinesSE.Contracts.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinesSE.Core.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public bool ConfigurationFound { get; private set; }

        readonly Dictionary<string, IConfigurationKeys> configurationCache
            = new Dictionary<string, IConfigurationKeys>();

        public T Get<T>(T defaultFallback)
            where T : IConfigurationKeys
        {
            if (GetFromCache(out T cached))
                return cached;

            cached = Parse(defaultFallback);
            configurationCache.Add(typeof(T).Name, cached);
            return cached;
        }

        T Parse<T>(T defaultFallback) where T : IConfigurationKeys
        {
            try
            {
                var deserialized = deserializer.Deserialize<IDictionary<string, T>>(configurationFile);

                if (deserialized.ContainsKey(typeof(T).Name))
                    return deserialized[typeof(T).Name];
                else if (deserialized.ContainsKey(typeof(T).Name.Replace("Configuration", "")))
                    return deserialized[typeof(T).Name.Replace("Configuration", "")];
                else
                    throw new Exception($"Configuration section {typeof(T).Name} not found");
            }
            catch (Exception e)
            {
                Log.Warn($"Type {typeof(T).Name} configuration deserialization failed", e);
                return defaultFallback;
            }
        }

        bool GetFromCache<T>(out T value)
            where T : IConfigurationKeys
        {
            value = default(T);
            if (!configurationCache.ContainsKey(typeof(T).Name))
                return false;

            value = (T)configurationCache[typeof(T).Name];
            return true;
        }

        string configurationFile;
        readonly IDeserializer deserializer;
        readonly ILog Log;

        const string CONFIGURATION_FILE_NAME = "Configuration.yml";

        public ConfigurationProvider(ILog log)
        {
            Log = log;
            deserializer = GetDeserializer();

            InitializeConfigurationFromFile();
        }

        void InitializeConfigurationFromFile()
        {
            var assemblyPath = Assembly
                .GetAssembly(typeof(ConfigurationProvider))
                .Location;
            var paths = new[]
            {
                Path.GetDirectoryName(assemblyPath),
                AppDomain.CurrentDomain.BaseDirectory
            };
            paths.Any(TryInitializeFromFile);
        }

        public ConfigurationProvider(ILog log, string configuration)
        {
            Log = log;
            deserializer = GetDeserializer();

            configurationFile = configuration;
            ConfigurationFound = true;
        }

        IDeserializer GetDeserializer()
            => new DeserializerBuilder()
            .WithNamingConvention(new PascalCaseNamingConvention())
            .IgnoreUnmatchedProperties()
            .Build();

        bool TryInitializeFromFile(string basePath)
        {
            var path = Path.Combine(basePath, CONFIGURATION_FILE_NAME);
            if (File.Exists(path))
            {
                configurationFile = File.ReadAllText(path);
                ConfigurationFound = true;
                Log.Debug($"Configuration file read from {path}");
                return true;
            }
            else
            {
                Log.Debug($"Configuration file not found at {path}");
                return false;
            }
        }
    }
}