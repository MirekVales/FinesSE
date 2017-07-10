using FinesSE.Contracts.Infrastructure;
using System;

namespace FinesSE.Outil.ParseMethods
{
    public class DoubleParse : IParseMethod
    {
        public Type ParsedType
            => typeof(double);

        public object Invoke(string input)
            => double.Parse(input);
    }
}
