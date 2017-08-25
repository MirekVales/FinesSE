using System;
using FinesSE.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace FinesSE.Drivers
{
    public class IEConfiguration : WebDriverConfiguration
    {
        public TimeSpan BrowserAttachTimeout { get; internal set; }
        public string BrowserCommandLineArguments { get; internal set; }
        public InternetExplorerElementScrollBehavior ElementScrollBehavior { get; internal set; }
        public bool EnableFullPageScreenshot { get; internal set; }
        public bool EnableNativeEvents { get; internal set; }
        public bool EnablePersistentHover { get; internal set; }
        public bool EnsureCleanSession { get; internal set; }
        public bool ForceCreateProcessApi { get; internal set; }
        public bool ForceShellWindowsApi { get; internal set; }
        public TimeSpan FileUploadDialogTimeout { get; internal set; }
        public bool IgnoreZoomLevel { get; internal set; }
        public string InitialBrowserUrl { get; internal set; }
        public bool IntroduceInstabilityByIgnoringProtectedModeSettings { get; internal set; }
        public InternetExplorerPageLoadStrategy PageLoadStrategy { get; internal set; }
        public Proxy Proxy { get; internal set; }
        public bool RequireWindowFocus { get; internal set; }
        public InternetExplorerUnexpectedAlertBehavior UnexpectedAlertBehavior { get; internal set; }
        public bool UsePerProcessProxy { get; internal set; }
    }
}