using FinesSE.Contracts;
using OpenQA.Selenium.Firefox;

namespace FinesSE.Drivers
{
    public class FirefoxConfiguration : WebDriverConfiguration
    {
        public string BrowserExecutableLocation { get; internal set; }
        public FirefoxDriverLogLevel LogLevel { get; internal set; }
        public bool UseLegacyImplementation { get; internal set; }
        public FirefoxProfile Profile { get; internal set; }
    }
}