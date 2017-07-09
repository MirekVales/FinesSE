using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FinesSE.Outil.Locators
{
    public class Name : ILocator
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public string Regex
            => "(name=)(.+)";

        public IEnumerable<IWebElement> Locate(string value)
            => DriverProvider.Get().FindElements(By.Name(value));
    }
}
