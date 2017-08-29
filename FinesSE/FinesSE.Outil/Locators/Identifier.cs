using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Identifier : ILocator
    {
        public IExecutionContext Context { get; set; }

        public string Id
            => "identifier";

        public string Regex
            => @"^(\(\?.+\))?(identifier=)(.+)";

        public LocatedElements Locate(string value, string modifiers)
            => Context
            .Driver
            .FindElements(By.Id(value))
            .AsLocatedElements(this, value, modifiers);
    }
}
