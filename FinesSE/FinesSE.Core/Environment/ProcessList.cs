using FinesSE.Contracts.Infrastructure;
using System.Collections.Generic;
using System.Diagnostics;

namespace FinesSE.Core.Environment
{
    public class ProcessList
    {
        public IEnumerable<ChildProcess> Processes { get; set; }

        public ProcessList()
        {
            Processes = new ChildProcess[0];
        }

        public void KillAllProcesses()
        {
            foreach (var processItem in Processes)
            {
                try
                {
                    var process = Process.GetProcessById(processItem.ProcessId);

                    if (string.Equals(
                        processItem.Name,
                        process.ProcessName,
                        System.StringComparison.InvariantCultureIgnoreCase))
                        process.Kill();
                }
                catch { }
            }
            Processes = new ChildProcess[0];
        }
    }
}