using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Name : ILocator
    {
        public IExecutionContext Context { get; set; }

        public string Id
            => "name";

        public string Regex
            => @"^(\(\?.+\))?(name=)(.+)";

        public LocatedElements Locate(string value, string modifiers)
        {
            var locatorModifiers = new LocatorModifiers(modifiers);

            return Context
                .Driver
                .FindElements(By.Name(value))
                .AsLocatedElements(this, value, locatorModifiers);
        }
    }
}
