using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace FinesSE.Drivers
{
    public class PhantomJS : IWebDriverActivator
    {
        public WebDrivers Id
            => WebDrivers.PhantomJS;

        public IWebDriver Activate(IConfigurationProvider provider)
        {
            var configuration = provider.Get<PhantomJSConfiguration>(null);
            if (configuration == null)
                return new PhantomJSDriver();
            
            return new PhantomJSDriver(
                configuration.WebDriverDirectory,
                GetOptions(configuration),
                configuration.CommandTimeout);
        }

        public PhantomJSOptions GetOptions(PhantomJSConfiguration configuration)
        {
            var options = new PhantomJSOptions();
            configuration.SetAdditionalCapability(options);

            return options;
        }
    }
}
