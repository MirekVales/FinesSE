using System;

namespace FinesSE.Contracts.Exceptions
{
    public class ParserNotFoundException : SlimException
    {
        public ParserNotFoundException(Type notFoundType)
            : base($"Parser for type '{nameof(notFoundType)}' is not defined")
        { }
    }
}
