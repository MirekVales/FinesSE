using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Contracts.Invokable
{
    public class LocatedElements
    {
        public ILocator Locator { get; }
        public string Parameter { get; }
        public LocatorModifiers Modifiers { get; }
        public IEnumerable<IWebElement> Elements { get; }

        public LocatedElements(
            ILocator locator,
            string parameter,
            LocatorModifiers modifiers,
            IEnumerable<IWebElement> elements)
        {
            Locator = locator;
            Parameter = parameter;
            Modifiers = modifiers;
            Elements = elements.ToArray();
        }

        public LocatedElements ConstraintCount(Predicate<int> countPredicate)
        {
            if (!countPredicate(Elements.Count()))
                throw new InvalidNumberOfWebElementsException(Elements.Count());

            return this;
        }

        public override string ToString()
            => Locator.Regex + " " + string.Join(" ,", Elements.Select(e => e.TagName));
    }
}
