using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FinesSE.Drivers
{
    public class Chrome : IWebDriverActivator
    {
        public WebDrivers Id
            => WebDrivers.Chrome;

        public IWebDriver Activate(IConfigurationProvider provider)
        {
            var configuration = provider.Get<ChromeConfiguration>(null);
            if (configuration == null)
                return new ChromeDriver();

            return new ChromeDriver(
                configuration.WebDriverDirectory,
                GetOptions(configuration),
                configuration.CommandTimeout);
        }

        public ChromeOptions GetOptions(ChromeConfiguration configuration)
        {
            var options = new ChromeOptions()
            {
                BinaryLocation = configuration.BinaryLocation,
                DebuggerAddress = configuration.DebuggerAddress,
                LeaveBrowserRunning = configuration.LeaveBrowserRunning,
                MinidumpPath = configuration.MinidumpPath,
                PerformanceLoggingPreferences = configuration.PerformanceLoggingPreferences,
                Proxy = configuration.Proxy
            };

            configuration.SetAdditionalCapability(options);

            return options;
        }
    }
}
