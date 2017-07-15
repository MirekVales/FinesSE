using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Name : ILocator
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public string Id
            => "name";

        public string Regex
            => "(name=)(.+)";

        public LocatedElements Locate(string value)
            => DriverProvider
            .Get()
            .FindElements(By.Name(value))
            .AsLocatedElements(this, value);
    }
}
