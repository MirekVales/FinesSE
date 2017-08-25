using OpenQA.Selenium;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IWebDriverActivator
    {
        WebDrivers Id { get; }

        IWebDriver Activate(IConfigurationProvider provider);
    }
}