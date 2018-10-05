using System.Collections.Generic;

namespace FinesSE.Soap.Infrastructure.DummyData
{
    public interface IDummyDataProvider
    {
        IEnumerable<string> Name { get; }

        string Get();
    }
}
