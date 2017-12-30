using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using System.Collections.Generic;

namespace FinesSE.Outil.Assertions
{
    public class SmallerThan : IVoidAction, IReportable
    {
        public string Name { get; } = "Smaller Than";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string first, string second)
        {
            if (first.ToDouble() >= second.ToDouble())
                throw new AssertionException(first + " is smaller than", second, WebDrivers.Default);
        }
    }
}
