using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Assertions
{
    public class NotExists : IStringAction, IReportable
    {
        public string Name { get; } = "Exists";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public string Invoke(LocatedElements elements)
        {
            if (elements.Elements.Any())
                throw new AssertionException("Element not expected to exists", "Element found", WebDrivers.Default);

            return "true";
        }
    }
}
