using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Environment;
using log4net;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Core.WebDriver
{
    public class WebDriverProvider : IWebDriverProvider
    {
        public ILog Log { get; set; }

        private readonly IKernel kernel;
        private readonly IConfigurationProvider configuration;

        public WebDrivers LastDriver { get; set; } = WebDrivers.Default;

        public IEnumerable<ChildProcess> BrowserProcesses { get => browserProcesses; }
        readonly List<ChildProcess> browserProcesses = new List<ChildProcess>();

        public WebDriverProvider(IKernel kernel, IConfigurationProvider configuration)
        {
            this.kernel = kernel;
            this.configuration = configuration;
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
                return TryActivate(driver);
            }
            return null;
        }

        private IWebDriver TryActivate(WebDrivers driver)
        {
            using (var processSeeker = new BrowserProcessSeeker(
                driver, p => browserProcesses.AddRange(p)))
            {
                var activator = kernel
                    .GetWebDriverActivators()
                    .FirstOrDefault(wa => wa.Id == driver);

                if (activator != null)
                    return activator.Activate(configuration);

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

        public void Dispose()
            => browserProcesses.ForEach(KillProcessSilently);

        void KillProcessSilently(ChildProcess process)
        {
            try
            {
                process.Kill();
            }
            catch { }
        }

        public void EndDriverProcess(WebDrivers driverType)
            => browserProcesses
            .Where(p => p.WebDriver == driverType)
            .ToList()
            .ForEach(KillProcessSilently);
    }
}