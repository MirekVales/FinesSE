using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.ParseMethods
{
    public class LocatedElementsParse : IParseMethod
    {
        public IKernel Kernel { get; set; }

        public Type ParsedType
            => typeof(LocatedElements);

        public object Invoke(string input)
        {
            Func<ILocator, string, bool> matches =
                (l, s) => Regex.IsMatch(s, l.Regex);
            var locator = Kernel.GetLocators().First(x => matches(x, input));
            return locator.Locate(Regex.Match(input, locator.Regex).Groups[2].Value);
        }
    }
}
