using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FinesSE.Core.WebDriver
{
    public class WebDriverProvider : IWebDriverProvider
    {
        public ILog Log { get; set; }

        private readonly Dictionary<WebDrivers, Func<IWebDriver>> factory;
        private readonly IConfigurationProvider configuration;

        public WebDrivers LastDriver { get; set; } = WebDrivers.Default;

        public WebDriverProvider(IConfigurationProvider configuration)
        {
            this.configuration = configuration;
            factory = DriverFactory();
        }

        private Dictionary<WebDrivers, Func<IWebDriver>> DriverFactory()
        {
            return new Dictionary<WebDrivers, Func<IWebDriver>>()
            {
                { WebDrivers.Chrome,    () => new OpenQA.Selenium.Chrome.ChromeDriver() },
                { WebDrivers.Edge,      () => new OpenQA.Selenium.Edge.EdgeDriver() },
                { WebDrivers.Firefox,   () => new OpenQA.Selenium.Firefox.FirefoxDriver() },
                { WebDrivers.IE,        () => new OpenQA.Selenium.IE.InternetExplorerDriver() },
                { WebDrivers.Opera,     () => new OpenQA.Selenium.Opera.OperaDriver() },
            };
        }

        private IWebDriver GetDriver()
        {
            var driver = LastDriver;
            if (driver == WebDrivers.Default)
                driver = configuration.Get(CoreConfiguration.Default).DefaultBrowser;

            if (driver.IsDynamic())
            {
                using (var e = new WebDriverNotFoundException(driver))
                    Log.Fatal($"Cannot construct driver for dynamic {driver}", e);
            }
            else
            {
                if (factory.ContainsKey(driver))
                    return factory[driver]();

                using (var e = new WebDriverNotFoundException(driver))
                    Log.Fatal($"Web driver factory not found for {driver}", e);
            }
            return null;
        }

        public void SetBrowser(WebDrivers driver)
            => LastDriver = driver;

        public IWebDriver Get(WebDrivers driver)
        {
            LastDriver = driver;
            return GetDriver();
        }
    }
}
