using System;
using System.Diagnostics;
using System.Management;

namespace FinesSE.Contracts.Infrastructure
{
    public struct ChildProcess
    {
        public ChildProcess(ManagementObject item, WebDrivers webDriver)
        {
            ProcessId = Convert.ToInt32(item["ProcessId"]);
            ParentProcessId = Convert.ToInt32(item["ProcessId"]);
            Name = Convert.ToString(item["Name"]);
            WebDriver = webDriver;
        }

        public int ProcessId { get; }
        public int ParentProcessId { get; }
        public string Name { get; }
        public WebDrivers WebDriver { get; set; }

        public void Kill()
            => Process.GetProcessById(ProcessId).Kill();
    }
}