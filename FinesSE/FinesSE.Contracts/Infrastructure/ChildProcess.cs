using System;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace FinesSE.Contracts.Infrastructure
{
    public class ChildProcess
    {
        public ChildProcess()
        {
        }

        public ChildProcess(ManagementObject item, WebDrivers webDriver)
        {
            ProcessId = Convert.ToInt32(item["ProcessId"]);
            ParentProcessId = Convert.ToInt32(item["ProcessId"]);
            Name = Convert.ToString(item["Name"]);
            ExecutablePath = Convert.ToString(item["ExecutablePath"]);
            WebDriver = webDriver;
            RegistrationDate = DateTime.UtcNow;
        }

        public int ProcessId { get; set; }
        public int ParentProcessId { get; set; }
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public WebDrivers WebDriver { get; set; }
        public DateTime RegistrationDate { get; set; }

        public void Kill()
           => Process.GetProcessById(ProcessId).Kill();
    }
}