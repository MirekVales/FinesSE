using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Outil.Assertions
{
    public class NotContains : IStringAction, IReportable
    {
        public string Name { get; } = "NotContains";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public string Invoke(string needle, string value)
        {
            if (value.Contains(needle))
                throw new AssertionException(value + " should not contain ", needle, WebDrivers.Default);

            return "true";
        }
    }
}
