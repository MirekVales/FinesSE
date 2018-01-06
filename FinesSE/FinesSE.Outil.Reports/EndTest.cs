using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;

namespace FinesSE.Outil.Reports
{
    public class EndTest : IVoidAction
    {
        public IReportBuilder ReportBuilder { get; set; }

        [EntryPoint]
        public void Invoke(string id, string status, string description)
            => ReportBuilder.EndTest(
                string.IsNullOrWhiteSpace(id) ? ReportBuilder.LastTestId : Guid.Parse(id),
                (LogStatus)Enum.Parse(typeof(LogStatus), status),
                description);
    }
}
