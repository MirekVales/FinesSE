using FinesSE.Contracts.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FinesSE.Core.Environment
{
    public class ProcessListStorage : IProcessListStorage
    {
        const string ProcessListFileName = "WebDriverProcesses.yaml";

        string GetFilePath => ProcessListFileName.GetRootedPath();

        public ILog Log { get; set; }

        readonly CoreConfiguration configuration;

        public ProcessListStorage()
        {
            configuration = CoreConfiguration.Default;
        }

        public ProcessListStorage(IConfigurationProvider ConfigurationProvider)
        {
            configuration = ConfigurationProvider.Get(CoreConfiguration.Default);
        }

        public void AddProcesses(IEnumerable<ChildProcess> processes)
        {
            var list = LoadFromDisk();
            list.Processes = list.Processes.Concat(processes);
            SaveToDisk(list);
        }

        public void CleanList()
        {
            var list = LoadFromDisk();
            KillAllProcesses(list.Processes);
            SaveToDisk(list);
        }

        void KillAllProcesses(IEnumerable<ChildProcess> processes)
        {
            if (configuration.ProcessTerminationEnabled)
            {
                foreach (var processItem in processes)
                {
                    try
                    {
                        var process = Process.GetProcessById(processItem.ProcessId);

                        Log.Debug($"Process {processItem.Name} PID {processItem.ProcessId} is evaluated for termination");

                        if (string.Equals(
                            processItem.Name,
                            process.ProcessName,
                            StringComparison.InvariantCultureIgnoreCase))
                        {
                            Log.Debug($"Process {processItem.Name} PID {processItem.ProcessId} is about to be terminated");
                            process.Kill();
                            Log.Debug($"Process {processItem.Name} PID {processItem.ProcessId} was terminated");
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"An exception when terminating process {processItem.Name} PID {processItem.ProcessId} {e.Message}");
                    }
                }
            }
            processes = new ChildProcess[0];
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