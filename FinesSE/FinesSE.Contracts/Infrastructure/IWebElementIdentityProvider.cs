using OpenQA.Selenium;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IWebElementIdentityProvider
    {
        string GetIdentifier(IWebDriver driver, IWebElement element);
    }
}
