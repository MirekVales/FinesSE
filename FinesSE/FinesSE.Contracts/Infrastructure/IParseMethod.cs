using System;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IParseMethod
    {
        Type ParsedType { get; }

        object Invoke(string input);
    }
}
