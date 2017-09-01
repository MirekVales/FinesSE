using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class XPath : ILocator
    {
        public IExecutionContext Context { get; set; }

        public string Id
            => "xpath";

        public string Regex
            => @"^(\(\?.+\))?(xpath=)(.+)";

        public LocatedElements Locate(string value, string modifiers)
        {
            var locatorModifiers = new LocatorModifiers(modifiers);

            return Context
                .Driver
                .FindElements(By.XPath(value))
                .AsLocatedElements(this, value, locatorModifiers);
        }
    }
}
