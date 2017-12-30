using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Outil.Assertions
{
    public class Equals : IVoidAction, IReportable
    {
        public string Name { get; } = "Equals";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string expected, string actual)
        {
            if (expected != actual)
                throw new AssertionException(expected, actual, WebDrivers.Default);
        }
    }
}
