using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace FinesSE.Core.Parsing
{
    public class ParameterParser : BaseParameterParser
    {
        public ParameterParser()
        {
            Set<string>(s => s);
            Set<int>(s => int.Parse(s));
            Set<Point>(x => Point.Parse(x));
            Set<IEnumerable<IWebElement>>(ParseLocator);
        }

        private object ParseLocator(string arg)
        {
            Func<ILocator, string, bool> matches =
                (l, s) => Regex.IsMatch(s, l.Regex);
            var locator = Kernel.GetLocators().First(x => matches(x, arg));
            return locator.Locate(Regex.Match(arg, locator.Regex).Groups[2].Value);
        }
    }
}
