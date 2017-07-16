using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class ExecutionContext : IExecutionContext
    {
        public IConfigurationProvider ConfigurationProvider { get; }
        public string TopicId => $"{currentBrowser}_{topicId}";
        public IWebDriver Driver => drivers[currentBrowser];
        public IEnumerable<WebDrivers> Drivers
            => drivers.Keys;

        private string topicId = "Default";
        private readonly ILog log;

        private WebDrivers currentBrowser;
        private readonly IWebDriverProvider driverProvider;
        private readonly Dictionary<WebDrivers, IWebDriver> drivers;
        
        private Dictionary<WebDrivers, IEnumerable<WebDrivers>> dynamicInitializers;

        public ExecutionContext(ILog log, IWebDriverProvider driverProvider, IConfigurationProvider configurationProvider)
        {
            this.log = log;
            this.driverProvider = driverProvider;
            ConfigurationProvider = configurationProvider;

            dynamicInitializers = GetDynamicInitializers();
            drivers = new Dictionary<WebDrivers, IWebDriver>();
            currentBrowser = ConfigurationProvider.Get(CoreConfiguration.Default).DefaultBrowser;
        }

        private Dictionary<WebDrivers, IEnumerable<WebDrivers>> GetDynamicInitializers()
        {
            return new Dictionary<WebDrivers, IEnumerable<WebDrivers>>()
            {
                { WebDrivers.Default, new WebDrivers[]{ ConfigurationProvider.Get(CoreConfiguration.Default).DefaultBrowser } },
                { WebDrivers.AllAvailable, GetAllAvailableBrowsers() },
                { WebDrivers.Random, GetRandomBrowser() }
            };
        }

        private IEnumerable<WebDrivers> GetRandomBrowser()
        {
            var available = GetAllAvailableBrowsers();
            return new[] { available.ElementAt(DateTime.Now.Millisecond % available.Count()) };
        }

        private IEnumerable<WebDrivers> GetAllAvailableBrowsers()
        {
            foreach (var name in Enum.GetNames(typeof(WebDrivers)))
            {
                if (((WebDrivers)Enum.Parse(typeof(WebDrivers), name)).IsDynamic())
                    continue;

                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{name}driver.exe")))
                    yield return (WebDrivers)Enum.Parse(typeof(WebDrivers), name);
            }
        }

        public void SetTopicId(string id)
            => topicId = id;

        public void SetBrowser(WebDrivers value)
        {
            var splitBrowsers = value.Split().ToList();
            var atomicDrivers = GetAtomicDrivers(splitBrowsers);
            foreach (var atomicDriver in atomicDrivers)
            {
                if (drivers.ContainsKey(atomicDriver))
                    continue;
                drivers.Add(atomicDriver, driverProvider.Get(atomicDriver));
            }
            currentBrowser = atomicDrivers.First();
        }

        private IEnumerable<WebDrivers> GetAtomicDrivers(List<WebDrivers> splitBrowsers)
        {
            foreach (var browser in splitBrowsers)
            {
                if (browser.IsDynamic())
                    foreach (var item in dynamicInitializers[browser])
                        yield return item;
                else
                    yield return browser;
            }
        }

        public void Dispose()
        {
            foreach (var driver in drivers.Values)
            {
                log.Debug($"Driver {driver} is about to be disposed");

                driver.CloseWindow();
                driver.Quit();
                driver.Dispose();
            }
            drivers.Clear();
        }
    }
}
