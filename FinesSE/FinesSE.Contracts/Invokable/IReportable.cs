using System.Collections.Generic;

namespace FinesSE.Contracts.Invokable
{
    public interface IReportable
    {
        string Name { get; }
        string Description { get; }
        IEnumerable<string> Category { get; }
    }
}
