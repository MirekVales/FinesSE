using FinesSE.Contracts.Infrastructure;
using System;

namespace FinesSE.Outil.ParseMethods
{
    public class StringParse : IParseMethod
    {
        public Type ParsedType
        => typeof(string);

        public object Invoke(string input)
            => input;
    }
}
