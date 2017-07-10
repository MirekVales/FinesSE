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
            if (Enum.TryParse(input.Replace(",", "|"), out WebDrivers parsed))
                return parsed;

            return WebDrivers.Default;
        }
    }
}
