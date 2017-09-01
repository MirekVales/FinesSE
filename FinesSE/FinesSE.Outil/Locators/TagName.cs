using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class TagName : ILocator
    {
        public IExecutionContext Context { get; set; }

        public string Id
            => "tagname";

        public string Regex
            => @"^(\(\?.+\))?(tagname=)(.+)";

        public LocatedElements Locate(string value, string modifiers)
        {
            var locatorModifiers = new LocatorModifiers(modifiers);

            return Context
                .Driver
                .FindElements(By.TagName(value))
                .AsLocatedElements(this, value, locatorModifiers);
        }
    }
}
