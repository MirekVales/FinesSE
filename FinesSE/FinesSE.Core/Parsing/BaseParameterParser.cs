using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;

namespace FinesSE.Core.Parsing
{
    public abstract class BaseParameterParser : IParameterParser
    {
        public ILog Log { get; set; }

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
                    using (var e = new ParserNotFoundException(type))
                        Log.Fatal($"Parameter parser was not found for {type}", e);
                }
            }
        }

        public void Set<T>(Func<string, object> parser)
            => Set(typeof(T), parser);

        public void Set(Type type, Func<string, object> parser)
        {
            if (parsers.ContainsKey(type))
                parsers[type] = parser;
            else
                parsers.Add(type, parser);
        }
    }
}
