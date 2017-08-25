using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Opera;

namespace FinesSE.Drivers
{
    public class Opera : IWebDriverActivator
    {
        public WebDrivers Id
            => WebDrivers.Opera;

        public IWebDriver Activate(IConfigurationProvider provider)
        {
            var configuration = provider.Get<OperaConfiguration>(null);
            if (configuration == null)
                return new OperaDriver();

            return new OperaDriver(
                configuration.WebDriverDirectory,
                GetOptions(configuration),
                configuration.CommandTimeout);
        }

        public OperaOptions GetOptions(OperaConfiguration configuration)
        {
            var options = new OperaOptions()
            {
                BinaryLocation = configuration.BinaryLocation,
                DebuggerAddress = configuration.DebuggerAddress,
                LeaveBrowserRunning = configuration.LeaveBrowserRunning,
                MinidumpPath = configuration.MinidumpPath,
                Proxy = configuration.Proxy
            };

            configuration.SetAdditionalCapability(options);

            return options;
        }
    }
}
