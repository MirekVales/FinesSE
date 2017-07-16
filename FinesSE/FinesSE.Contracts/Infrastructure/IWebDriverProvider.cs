using OpenQA.Selenium;
using System;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IWebDriverProvider
    {
        IWebDriver Get(WebDrivers driver);
    }
}
