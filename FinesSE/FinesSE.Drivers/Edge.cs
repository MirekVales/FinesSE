using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace FinesSE.Drivers
{
    public class Edge: IWebDriverActivator
    {
        public WebDrivers Id
            => WebDrivers.Edge;

        public IWebDriver Activate(IConfigurationProvider provider)
        {
            var configuration = provider.Get<EdgeConfiguration>(null);
            if (configuration == null)
                return new EdgeDriver();

            return new EdgeDriver(
                configuration.WebDriverDirectory,
                GetOptions(configuration),
                configuration.CommandTimeout);
        }

        public EdgeOptions GetOptions(EdgeConfiguration configuration)
        {
            var options = new EdgeOptions()
            {
                PageLoadStrategy = configuration.PageLoadStrategy
            };

            configuration.SetAdditionalCapability(options);

            return options;
        }
    }
}
