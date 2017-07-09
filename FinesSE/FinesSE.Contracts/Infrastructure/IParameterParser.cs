using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IParameterParser
    {
        void Set<T>(Func<string, object> parser);

        IEnumerable<object> Parse(IEnumerable<string> objs, IEnumerable<Type> types);
    }
}
