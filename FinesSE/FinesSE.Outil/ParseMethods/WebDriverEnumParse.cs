using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using System;

namespace FinesSE.Outil.ParseMethods
{
    public class WebDriverEnumParse : IParseMethod
    {
        public Type ParsedType
            => typeof(WebDrivers);

        public object Invoke(string input)
        {
            var values = input.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var parsed = WebDrivers.Default;
            foreach (var value in values)
            {
                if (Enum.TryParse(value.Trim(' '), out WebDrivers nextValue))
                {
                    if (parsed == WebDrivers.Default)
                        parsed = nextValue;
                    else
                        parsed |= nextValue;
                }
            }

            return parsed;
        }
    }
}
