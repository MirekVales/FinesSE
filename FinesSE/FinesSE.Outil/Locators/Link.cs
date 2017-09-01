using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Link : ILocator
    {
        public IExecutionContext Context { get; set; }

        public string Id
            => "link";

        public string Regex
            => @"^(\(\?.+\))?(link=)(.+)";

        public LocatedElements Locate(string value, string modifiers)
        {
            var locatorModifiers = new LocatorModifiers(modifiers);

            return Context
                .Driver
                .FindElements(By.LinkText(value), locatorModifiers)
                .AsLocatedElements(this, value, locatorModifiers);
        }
    }
}
