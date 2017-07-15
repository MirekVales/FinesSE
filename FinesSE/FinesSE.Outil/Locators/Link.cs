using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Locators
{
    public class Link : ILocator
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public string Id
            => "link";

        public string Regex
            => "(link=)(.+)";

        public LocatedElements Locate(string value)
            => DriverProvider
            .Get()
            .FindElements(By.LinkText(value))
            .AsLocatedElements(this, value);
    }
}
