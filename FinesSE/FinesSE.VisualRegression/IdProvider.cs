using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using System.IO;

namespace FinesSE.VisualRegression
{
    public class IdProvider : IWebElementIdentityProvider
    {
        public string GetIdentifier(IWebDriver driver, IWebElement element)
        {
            var id = string.Join("_", 
                element.TagName, 
                element.GetAttribute("id"), 
                element.GetAttribute("name"), 
                element.GetAttribute("class"), 
                element.GetAttribute("style"),
                element.GetAttribute("title"),
                element.Text);
            var safeId = string.Join("_", id.Split(Path.GetInvalidFileNameChars()));
            return safeId.Length > 70 ? safeId.Substring(0, 70) : safeId;
        }
    }
}
