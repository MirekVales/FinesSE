using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace FinesSE.Core.Environment
{
    public class BrowserProcessSeeker : IDisposable
    {
        readonly WebDrivers driverType;
        readonly Action<IEnumerable<ChildProcess>> endAction;

        readonly IEnumerable<ChildProcess> startProcesses;

        public BrowserProcessSeeker(
            IProcessListStorage storage,
            WebDrivers driverType,
            Action<IEnumerable<ChildProcess>> endAction)
        {
            this.driverType = driverType;
            this.endAction = endAction;
            startProcesses = GetChildProcesses(PID, driverType).ToArray();
            storage.AddProcesses(startProcesses);
        }

        public void Dispose()
        {
            var incrementalProcesses = GetChildProcesses(PID, driverType)
                                        .Except(startProcesses)
                                        .ToArray();
            endAction(incrementalProcesses);
        }

        public static int PID => Process.GetCurrentProcess().Id;

        public static IEnumerable<ChildProcess> GetChildProcesses(int pid, WebDrivers driverType)
        {
            var query = new ObjectQuery($"select * from win32_process where ParentProcessId={pid}");
            using (var searcher = new ManagementObjectSearcher(query))
            using (var collection = searcher.Get())
                foreach (ManagementObject item in collection)
                    using (item)
                        yield return new ChildProcess(item, driverType);
        }
    }
}