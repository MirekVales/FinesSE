using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Contracts.Invokable
{
    public class LocatedElements
    {
        public ILocator Locator { get; }
        public string Parameter { get; }
        public IEnumerable<IWebElement> Elements { get; }

        public LocatedElements(ILocator locator, string parameter, IEnumerable<IWebElement> elements)
        {
            Locator = locator;
            Parameter = parameter;
            Elements = elements.ToArray();
        }
    }
}
