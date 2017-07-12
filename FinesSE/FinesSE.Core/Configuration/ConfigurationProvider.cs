using FinesSE.Contracts.Infrastructure;
using log4net;
using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinesSE.Core.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly ILog Log;

        public bool ConfigurationFound { get; private set; }

        public T Get<T>(T defaultFallback)
            where T : IConfigurationKeys
        {
            try
            {
                return deserializer.Deserialize<T>(configurationFile);
            }
            catch (Exception e)
            {
                Log.Warn($"Type {nameof(T)} configuration deserialization failed",e);
                return defaultFallback;
            }
        }

        private string configurationFile;
        private readonly Deserializer deserializer;

        public ConfigurationProvider(ILog log)
        {
            Log = log;
            deserializer = GetDeserializer();

            const string CONFIGURATION_FILE_NAME = "Configuration.yml";
            Initialize(AppDomain.CurrentDomain.BaseDirectory, CONFIGURATION_FILE_NAME);
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

        private void Initialize(string basePath, string configurationFileName)
        {
            var path = Path.Combine(basePath, configurationFileName);
            if (File.Exists(path))
            {
                configurationFile = File.ReadAllText(path);
                ConfigurationFound = true;
                Log.Debug($"Configuration file read from {path}");
            }
            else
            {
                Log.Debug($"Configuration file not found at {path}");
            }
        }
    }
}
