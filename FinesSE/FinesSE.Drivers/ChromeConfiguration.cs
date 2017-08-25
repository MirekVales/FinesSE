using FinesSE.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FinesSE.Drivers
{
    public class ChromeConfiguration : WebDriverConfiguration
    {
        public string BinaryLocation { get; set; }
        public bool LeaveBrowserRunning { get; set; }
        public string DebuggerAddress { get; set; }
        public string MinidumpPath { get; internal set; }
        public ChromePerformanceLoggingPreferences PerformanceLoggingPreferences { get; internal set; }
        public Proxy Proxy { get; internal set; }
    }
}
