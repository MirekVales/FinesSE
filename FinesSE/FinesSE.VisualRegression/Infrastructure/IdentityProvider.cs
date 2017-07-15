using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class IdentityProvider : IWebElementIdentityProvider
    {
        public const int MAX_ID_LENGTH = 70;

        public string GetIdentifier(IWebDriver driver, LocatedElements elements, IWebElement element)
        {
            var replacedCharacters = Path.GetInvalidFileNameChars()
                .Concat(new[] { '_' })
                .ToArray();
            var id = string.Join("_",
                GetElementIndex(elements, element),
                element.TagName,
                elements.Locator.Id,
                elements.Parameter);
            var safeId = string.Join("_", id.Split(replacedCharacters, StringSplitOptions.RemoveEmptyEntries));

            return safeId.Length > MAX_ID_LENGTH ? safeId.Substring(0, MAX_ID_LENGTH) : safeId;
        }

        private int GetElementIndex(LocatedElements elements, IWebElement element)
            => elements
            .Elements
            .ToList()
            .IndexOf(element) + 1;
    }
}
