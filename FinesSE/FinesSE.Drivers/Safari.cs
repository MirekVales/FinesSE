using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using System.Collections.Generic;

namespace FinesSE.Drivers
{
    public class Safari : IWebDriverActivator
    {
        public WebDrivers Id
            => WebDrivers.Safari;

        public IEnumerable<string> GetExecutableNamePatterns()
        {
            yield return "safari";
            yield return "safaridriver";
        }

        public IWebDriver Activate(IConfigurationProvider provider)
        {
            var configuration = provider.Get<SafariConfiguration>(null);
            if (configuration == null)
                return new SafariDriver();

            return new SafariDriver(
                configuration.WebDriverDirectory,
                GetOptions(configuration),
                configuration.CommandTimeout);
        }

        public SafariOptions GetOptions(SafariConfiguration configuration)
        {
            var options = new SafariOptions();
            configuration.SetAdditionalCapability(options);

            return options;
        }
    }
}
