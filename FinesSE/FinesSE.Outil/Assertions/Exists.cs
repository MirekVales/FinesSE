using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Assertions
{
    public class Exists : IVoidAction, IReportable
    {
        public string Name { get; } = "Exists";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(LocatedElements elements)
        {
            if (!elements.Elements.Any())
                throw new AssertionException("Element expected to exists", "No element found", WebDrivers.Default);
        }
    }
}
