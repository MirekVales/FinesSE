using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace FinesSE.Drivers
{
    public class Firefox : IWebDriverActivator
    {
        public WebDrivers Id
            => WebDrivers.Firefox;

        public IEnumerable<string> GetExecutableNamePatterns()
        {
            yield return "firefox";
            yield return "geckodriver";
        }

        public IWebDriver Activate(IConfigurationProvider provider)
        {
            var configuration = provider.Get<FirefoxConfiguration>(null);
            if (configuration == null)
                return new FirefoxDriver();

            return new FirefoxDriver(GetOptions(configuration));
        }

        public FirefoxOptions GetOptions(FirefoxConfiguration configuration)
        {
            var options = new FirefoxOptions()
            {
                BrowserExecutableLocation = configuration.BrowserExecutableLocation,
                LogLevel = configuration.LogLevel,
                Profile = configuration.Profile,
                UseLegacyImplementation = configuration.UseLegacyImplementation
            };

            configuration.SetAdditionalCapability(options);

            return options;
        }
    }
}
