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
            var match = Regex.Match(input, locator.Regex);
            var containsModifiers = match.Groups.Count > 3;

            var modifiers = containsModifiers  ? match.Groups[1].Value : "";
            var value = containsModifiers ? match.Groups[3].Value : match.Groups[2].Value;
            return locator.Locate(value, modifiers);
        }
    }
}
