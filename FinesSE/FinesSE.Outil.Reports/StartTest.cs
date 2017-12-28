using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;

namespace FinesSE.Outil.Reports
{
    public class StartTest : IStringAction
    {
        public IReportBuilder ReportBuilder { get; set; }

        [EntryPoint]
        public string Invoke(string name, string description)
        {
            var testId = Guid.NewGuid();
            ReportBuilder.StartTest(testId, name, description);
            return testId.ToString();
        }
    }
}
