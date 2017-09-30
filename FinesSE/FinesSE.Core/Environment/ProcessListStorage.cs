using FinesSE.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinesSE.Core.Environment
{
    public class ProcessListStorage : IProcessListStorage
    {
        const string ProcessListFileName = "WebDriverProcesses.yaml";

        string GetFilePath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ProcessListFileName);

        public void AddProcesses(IEnumerable<ChildProcess> processes)
        {
            var list = LoadFromDisk();
            list.Processes = list.Processes.Concat(processes);
            SaveToDisk(list);
        }

        public void CleanList()
        {
            var list = LoadFromDisk();
            list.KillAllProcesses();
            SaveToDisk(list);
        }

        void SaveToDisk(ProcessList list)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(new PascalCaseNamingConvention())
                .Build();

            File.WriteAllText(GetFilePath, serializer.Serialize(list));
        }

        ProcessList LoadFromDisk()
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