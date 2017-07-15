using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Css : ILocator
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public string Regex
            => "(css=)(.+)";

        public LocatedElements Locate(string value)
            => DriverProvider
            .Get()
            .FindElements(By.CssSelector(value))
            .AsLocatedElements(this, value);
    }
}
