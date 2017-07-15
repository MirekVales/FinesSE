using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IWebElementIdentityProvider
    {
        string GetIdentifier(IWebDriver driver, LocatedElements elements, IWebElement element);
    }
}
