using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Identifier : ILocator
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public string Id
            => "identifier";

        public string Regex
            => "(identifier=)(.+)";

        public LocatedElements Locate(string value)
            => DriverProvider
            .Get()
            .FindElements(By.Id(value))
            .AsLocatedElements(this, value);
    }
}
