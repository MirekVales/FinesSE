using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Css : ILocator
    {
        public IExecutionContext Context { get; set; }

        public string Id
            => "css";

        public string Regex
            => @"^(\(\?.+\))?(css=)(.+)";

        public LocatedElements Locate(string value, string modifiers)
        {
            var locatorModifiers = new LocatorModifiers(modifiers);

            return Context
                .Driver
                .FindElements(By.CssSelector(value), locatorModifiers)
                .AsLocatedElements(this, value, locatorModifiers);
        }
    }
}
