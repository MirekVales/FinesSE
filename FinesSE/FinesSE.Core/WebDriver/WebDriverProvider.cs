using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FinesSE.Core.WebDriver
{
    public class WebDriverProvider : IWebDriverProvider
    {
        private readonly Lazy<IWebDriver> driver;
        private readonly Dictionary<WebDrivers, Func<IWebDriver>> factory;
        private readonly IConfigurationProvider configuration;

        public WebDrivers CurrentDriver { get; set; } = WebDrivers.Default;

#warning TBR EContext
        public string TopicId { get; set; } = "Default";

        public WebDriverProvider(IConfigurationProvider configuration)
        {
            this.configuration = configuration;
            driver = new Lazy<IWebDriver>(Initialize);
            factory = InitializeFactory();
        }

        private Dictionary<WebDrivers, Func<IWebDriver>> InitializeFactory()
        {
            return new Dictionary<WebDrivers, Func<IWebDriver>>()
            {
                { WebDrivers.Chrome,    () => new OpenQA.Selenium.Chrome.ChromeDriver() },
                { WebDrivers.Edge,      () => new OpenQA.Selenium.Edge.EdgeDriver() },
                { WebDrivers.FireFox,   () => new OpenQA.Selenium.Firefox.FirefoxDriver() },
                { WebDrivers.IE,        () => new OpenQA.Selenium.IE.InternetExplorerDriver() },
                { WebDrivers.Opera,     () => new OpenQA.Selenium.Opera.OperaDriver() },
            };
        }

        private IWebDriver Initialize()
        {
            var driver = CurrentDriver;
            if (driver == WebDrivers.Default)
                driver = configuration.Get(CoreConfiguration.Default).DefaultBrowser;

            if (factory.ContainsKey(driver))
                return factory[driver]();

            throw new WebDriverNotFoundException(driver);
        }

        public void SetBrowser(WebDrivers driver)
            => CurrentDriver = driver;

        public IWebDriver Get()
            => driver.Value;

        public void Dispose()
        {
            if (driver.IsValueCreated)
            {
                driver.Value.CloseWindow();
                driver.Value.Quit();
                driver.Value.Dispose();
            }
        }
    }
}
