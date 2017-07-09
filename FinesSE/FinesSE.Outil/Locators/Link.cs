using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace FinesSE.Outil.Locators
{
    public class Link : ILocator
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public string Regex
            => "(link=)(.+)";

        public IEnumerable<IWebElement> Locate(string value)
            => DriverProvider.Get().FindElements(By.LinkText(value));
    }
}
