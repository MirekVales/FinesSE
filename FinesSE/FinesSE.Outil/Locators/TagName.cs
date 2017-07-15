using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class TagName : ILocator
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public string Regex
            => "(tagname=)(.+)";

        public LocatedElements Locate(string value)
            => DriverProvider
            .Get()
            .FindElements(By.TagName(value))
            .AsLocatedElements(this, value);
    }
}
