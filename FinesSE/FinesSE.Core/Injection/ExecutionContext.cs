using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
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
        public IWebDriver Driver => GetCurrentDriver();
        public IEnumerable<WebDrivers> Drivers => drivers.Keys;
        public IInvoker Invoker { get; set; }

        string topicId = "Default";
        readonly ILog log;

        WebDrivers currentBrowser;
        readonly IWebDriverProvider driverProvider;
        readonly Dictionary<WebDrivers, IWebDriver> drivers;

        Dictionary<WebDrivers, IEnumerable<WebDrivers>> dynamicInitializers;

        public ExecutionContext(ILog log, IWebDriverProvider driverProvider, IConfigurationProvider configurationProvider)
        {
            this.log = log;
            this.driverProvider = driverProvider;
            ConfigurationProvider = configurationProvider;

            dynamicInitializers = GetDynamicInitializers();
            drivers = new Dictionary<WebDrivers, IWebDriver>();
            currentBrowser = ConfigurationProvider.Get(CoreConfiguration.Default).DefaultBrowser;
        }

        readonly Stack<BranchType> branches = new Stack<BranchType>();

        public void AddWorkflowBranch(BranchType branchType)
        {
            if (branchType == BranchType.Close
                && branches.Any()
                && branches.Peek() == BranchType.Open)
                branches.Pop();
            else
                branches.Push(branchType);
        }

        public bool IsActionIgnored()
            => branches.Any() && branches.Peek() == BranchType.Open;

        Dictionary<WebDrivers, IEnumerable<WebDrivers>> GetDynamicInitializers()
        {
            return new Dictionary<WebDrivers, IEnumerable<WebDrivers>>
            {
                { WebDrivers.Default, new WebDrivers[]{ ConfigurationProvider.Get(CoreConfiguration.Default).DefaultBrowser } },
                { WebDrivers.AllAvailable, GetAllAvailableBrowsers() },
                { WebDrivers.Random, GetRandomBrowser() }
            };
        }

        IEnumerable<WebDrivers> GetRandomBrowser()
        {
            var available = GetAllAvailableBrowsers();
            return available.Any()
                ? new[] { available.ElementAt(DateTime.Now.Millisecond % available.Count()) }
                : new WebDrivers[0];
        }

        IEnumerable<WebDrivers> GetAllAvailableBrowsers()
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

        IEnumerable<WebDrivers> GetAtomicDrivers(List<WebDrivers> splitBrowsers)
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

        IWebDriver GetCurrentDriver()
        {
            if (!drivers.ContainsKey(currentBrowser))
                SetBrowser(currentBrowser);

            if (drivers.ContainsKey(currentBrowser))
                return drivers[currentBrowser];

            log.Debug($"Driver {currentBrowser} not found in {drivers.Count} drivers");
            throw new WebDriverNotFoundException(currentBrowser);
        }

        public void Dispose()
        {
            foreach (var driver in drivers)
            {
                log.Debug($"Driver {driver.Value} is about to be disposed");

                driver.Value.Quit();
                driver.Value.Dispose();
                driverProvider.EndDriverProcess(driver.Key);

                log.Debug($"Driver {driver.Value} disposed");
            }
            drivers.Clear();
        }
    }
}