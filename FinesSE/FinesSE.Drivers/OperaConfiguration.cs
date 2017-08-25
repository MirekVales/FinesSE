using FinesSE.Contracts;
using OpenQA.Selenium;

namespace FinesSE.Drivers
{
    public class OperaConfiguration : WebDriverConfiguration
    {
        public string BinaryLocation { get; internal set; }
        public string DebuggerAddress { get; internal set; }
        public bool LeaveBrowserRunning { get; internal set; }
        public string MinidumpPath { get; internal set; }
        public Proxy Proxy { get; internal set; }
    }
}