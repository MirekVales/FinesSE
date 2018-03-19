using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.Assertions
{
    public class RegexMatches : IVoidAction, IReportable
    {
        public string Name { get; } = "RegexMatches";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string value, string regex)
        {
            if (!Regex.IsMatch(value, regex))
                throw new AssertionException(value + " should match regex ", regex, WebDrivers.Default);
        }
    }
}
