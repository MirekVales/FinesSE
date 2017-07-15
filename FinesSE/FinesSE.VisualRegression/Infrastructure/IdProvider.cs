using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class IdProvider : IWebElementIdentityProvider
    {
        public const int MAX_ID_LENGTH = 70;

        public string GetIdentifier(IWebDriver driver, IWebElement element)
        {
            var replacedCharacters = Path.GetInvalidFileNameChars()
                .Concat(new[] {'_'})
                .ToArray();

            var id = string.Join("_",
                element.TagName,
                element.GetAttribute("id"),
                element.GetAttribute("name"),
                element.GetAttribute("class"));

            var safeId = string.Join("_", id.Split(replacedCharacters, StringSplitOptions.RemoveEmptyEntries));

            return safeId.Length > MAX_ID_LENGTH ? safeId.Substring(0, MAX_ID_LENGTH) : safeId;
        }
    }
}
