using FinesSE.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinesSE.Core.Environment
{
    public class ProcessList
    {
        public ChildProcess[] Processes { get; set; }

        [YamlIgnore]
        public const string ProcessListFileName = "WebDriverProcesses.yaml";

        public ProcessList()
        {
            Processes = new ChildProcess[0];
        }

        public static string GetFilePath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ProcessListFileName);

        public void AddProcesses(IEnumerable<ChildProcess> processes)
        {
            Processes = Processes.Concat(processes).ToArray();
            SaveToDisk();
        }

        public void SaveToDisk()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(new PascalCaseNamingConvention())
                .Build();

            File.WriteAllText(GetFilePath, serializer.Serialize(this));
        }

        public void CleanList()
        {
            KillAllProcesses();
            SaveToDisk();
        }

        void KillAllProcesses()
        {
            foreach (var process in Processes)
            {
                try
                {
                    process.Kill();
                }
                catch { }
            }
            Processes = new ChildProcess[0];
        }

        public static ProcessList LoadFromDisk()
        {
            if (!File.Exists(GetFilePath))
                return new ProcessList();

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new PascalCaseNamingConvention())
                .Build();

            return deserializer
                .Deserialize<ProcessList>(File.ReadAllText(GetFilePath));
        }
    }
}
