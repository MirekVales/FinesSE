using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            var processes = new HashSet<ChildProcess>();

            try
            {
                var query = new ObjectQuery($"select * from win32_process where ParentProcessId={pid}");
                using (var searcher = new ManagementObjectSearcher(query))
                using (var collection = searcher.Get())
                    foreach (ManagementObject item in collection)
                    {
                        using (item)
                        {
                            var childProcess = new ChildProcess(item, driverType);
                            var processExecutable = Path.GetFileNameWithoutExtension(childProcess.ExecutablePath);
                            if (WebDriverExecutableNames().Any(
                                n => string.Equals(
                                    processExecutable,
                                    n,
                                    StringComparison.InvariantCultureIgnoreCase)))
                                processes.Add(childProcess);
                        }
                    }
            }
            catch { }

            return processes;
        }

        public static IEnumerable<string> WebDriverExecutableNames()
        {
            yield return "Chrome";
            yield return "Edge";
            yield return "Firefox";
            yield return "Gecko";
            yield return "GeckoDriver";
            yield return "IE";
            yield return "Opera";
            yield return "PhantomJS";
            yield return "Safari";
        }
    }
}