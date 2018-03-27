using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FinesSE.Contracts.Infrastructure
{
    public class LocatorModifiers
    {
        public LocatorModifiers(string modifiers)
        {
            SetParsers();
            Parse(modifiers);
        }

        Dictionary<string, Action<string>> modifierParsers;

        private void SetParsers()
        {
            modifierParsers = new Dictionary<string, Action<string>>()
            { };
        }

        private void Parse(string modifiers)
        {
            foreach (var parser in modifierParsers)
            {
                var match = Regex.Match(modifiers, parser.Key);
                if (match.Success)
                {
                    parser.Value(match.Groups[2].Value);
                    return;
                }
            }
        }
    }
}
