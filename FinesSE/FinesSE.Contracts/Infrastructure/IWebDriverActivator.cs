using OpenQA.Selenium;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IWebDriverActivator
    {
        WebDrivers Id { get; }

        IEnumerable<string> GetExecutableNamePatterns();

        IWebDriver Activate(IConfigurationProvider provider);
    }
}