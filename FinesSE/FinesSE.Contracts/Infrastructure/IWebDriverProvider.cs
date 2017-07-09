using OpenQA.Selenium;
using System;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IWebDriverProvider : IDisposable
    {
        IWebDriver Get();
    }
}
