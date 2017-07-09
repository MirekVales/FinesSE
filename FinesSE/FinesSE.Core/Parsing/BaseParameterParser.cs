using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using System;
using System.Collections.Generic;

namespace FinesSE.Core.Parsing
{
    public abstract class BaseParameterParser : IParameterParser
    {
        private readonly Dictionary<Type, Func<string, object>> parsers;

        public BaseParameterParser()
        {
            parsers = new Dictionary<Type, Func<string, object>>();
        }

        public IKernel Kernel { get; set; }

        public IEnumerable<object> Parse(IEnumerable<string> objs, IEnumerable<Type> types)
        {
            var objects = new Queue<string>(objs);
            foreach (var type in types)
            {
                if (parsers.ContainsKey(type))
                {
                    yield return parsers[type](objects.Dequeue());
                }
                else
                {
                    throw new ParserNotFoundException(type);
                }
            }
        }

        public void Set<T>(Func<string, object> parser)
        {
            if (parsers.ContainsKey(typeof(T)))
                parsers[typeof(T)] = parser;
            else
                parsers.Add(typeof(T), parser);
        }
    }
}
