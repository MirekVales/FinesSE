using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using System;
using System.Linq;

namespace FinesSE.Core
{
    public class CoreConfiguration : IConfigurationKeys
    {
        public WebDrivers DefaultBrowser { get; set; }
        public TimeSpan WaitForDocumentCompleteState { get; set; }
        public bool LogToFile { get; set; }
        public string LogPath { get; set; }
        public string LogPattern { get; set; }
        public BrowserSize DefaultBrowserSize { get; set; }
        public BrowserSize[] BrowserSizes { get; set; }

        public static CoreConfiguration Default
            => new CoreConfiguration()
            {
                DefaultBrowser = WebDrivers.Chrome,
                WaitForDocumentCompleteState = TimeSpan.FromSeconds(1),
                LogToFile = false,
                LogPattern = "%date [%level] %message",
                DefaultBrowserSize = new BrowserSize("Default", 1024, 768),
                BrowserSizes = BrowserSize.GetDefaultBrowserSizes().ToArray()
            };
    }
}