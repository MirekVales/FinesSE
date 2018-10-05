using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using System.Collections.Generic;

namespace FinesSE.Outil.Assertions
{
    public class GreaterThan : IStringAction, IReportable
    {
        public string Name { get; } = "Greater Than";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public string Invoke(string first, string second)
        {
            if (first.ToDouble() <= second.ToDouble())
                throw new AssertionException(first + " is greater than", second, WebDrivers.Default);

            return "true";
        }
    }
}
