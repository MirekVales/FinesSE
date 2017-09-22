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
            var cached = GetFromCache<T>();
            if (cached != null)
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
                Log.Warn($"Type {nameof(T)} configuration deserialization failed", e);
                return defaultFallback;
            }
        }

        T GetFromCache<T>()
        where T : IConfigurationKeys
        {
            if (!configurationCache.ContainsKey(typeof(T).Name))
                return default(T);

            return (T)configurationCache[typeof(T).Name];
        }

        private string configurationFile;
        private readonly Deserializer deserializer;
        private readonly ILog Log;

        const string CONFIGURATION_FILE_NAME = "Configuration.yml";

        public ConfigurationProvider(ILog log)
        {
            Log = log;
            deserializer = GetDeserializer();

            InitializeConfigurationFromFile();
        }

        private void InitializeConfigurationFromFile()
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

        private Deserializer GetDeserializer()
            => new DeserializerBuilder()
            .WithNamingConvention(new PascalCaseNamingConvention())
            .IgnoreUnmatchedProperties()
            .Build();

        private bool TryInitializeFromFile(string basePath)
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