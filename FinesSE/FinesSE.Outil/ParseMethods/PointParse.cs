using FinesSE.Contracts.Infrastructure;
using System;
using System.Windows;

namespace FinesSE.Outil.ParseMethods
{
    public class PointParse : IParseMethod
    {
        public Type ParsedType
            => typeof(Point);

        public object Invoke(string input)
            => Point.Parse(input);
    }
}
