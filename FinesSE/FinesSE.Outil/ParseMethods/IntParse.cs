using FinesSE.Contracts.Infrastructure;
using System;

namespace FinesSE.Outil.ParseMethods
{
    public class IntParse : IParseMethod
    {
        public Type ParsedType
            => typeof(int);

        public object Invoke(string input)
            => int.Parse(input);
    }
}
