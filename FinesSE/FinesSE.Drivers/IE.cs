﻿using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Collections.Generic;

namespace FinesSE.Drivers
{
    public class IE : IWebDriverActivator
    {
        public WebDrivers Id
            => WebDrivers.IE;

        public IEnumerable<string> GetExecutableNamePatterns()
        {
            yield return "ie";
            yield return "iedriverserver";
        }

        public IWebDriver Activate(IConfigurationProvider provider)
        {
            var configuration = provider.Get<IEConfiguration>(null);
            if (configuration == null)
                return new InternetExplorerDriver();

            return new InternetExplorerDriver(
                configuration.WebDriverDirectory,
                GetOptions(configuration),
                configuration.CommandTimeout);
        }

        public InternetExplorerOptions GetOptions(IEConfiguration configuration)
        {
            var options = new InternetExplorerOptions()
            {
                BrowserAttachTimeout = configuration.BrowserAttachTimeout,
                BrowserCommandLineArguments = configuration.BrowserCommandLineArguments,
                ElementScrollBehavior = configuration.ElementScrollBehavior,
                EnableNativeEvents = configuration.EnableNativeEvents,
                EnablePersistentHover = configuration.EnablePersistentHover,
                EnsureCleanSession = configuration.EnsureCleanSession,
                FileUploadDialogTimeout = configuration.FileUploadDialogTimeout,
                ForceCreateProcessApi = configuration.ForceCreateProcessApi,
                ForceShellWindowsApi = configuration.ForceShellWindowsApi,
                IgnoreZoomLevel = configuration.IgnoreZoomLevel,
                InitialBrowserUrl = configuration.InitialBrowserUrl,
                IntroduceInstabilityByIgnoringProtectedModeSettings = configuration.IntroduceInstabilityByIgnoringProtectedModeSettings,
                PageLoadStrategy = configuration.PageLoadStrategy,
                Proxy = configuration.Proxy,
                RequireWindowFocus = configuration.RequireWindowFocus,
                UsePerProcessProxy = configuration.UsePerProcessProxy
            };

            configuration.SetAdditionalCapability(options);

            return options;
        }
    }
}
