using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using System.IO;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class IdProvider : IWebElementIdentityProvider
    {
        public string GetIdentifier(IWebDriver driver, IWebElement element)
        {
            var id = string.Join("_",
                element.TagName,
                element.GetAttribute("id"),
                element.GetAttribute("name"),
                element.GetAttribute("class"));
            var safeId = string.Join("_", id.Split(Path.GetInvalidFileNameChars(), '_'));
            return safeId.Length > 70 ? safeId.Substring(0, 70) : safeId;
        }
    }
}
