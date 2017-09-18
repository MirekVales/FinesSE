using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IWebDriverProvider : IDisposable
    {
        IWebDriver Get(WebDrivers driver);

        IEnumerable<ChildProcess> BrowserProcesses { get; }

        void EndDriverProcess(WebDrivers driverType);
    }
}
