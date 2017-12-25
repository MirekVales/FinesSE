using FinesSE.Contracts.Infrastructure;
using FinesSE.Reports.Infrastructure;

namespace FinesSE.Reports
{
    public class ReportsConfiguration : IConfigurationKeys
    {
        public string ReportsFolder { get; set; }
        public bool ReportEnabled { get; set; }
        public string ReportStyleFile { get; set; }
        public UrlFormat UrlFormat { get; set; } 
        public Tags Tags { get; set; }

        public static ReportsConfiguration Default =>
            new ReportsConfiguration()
            {
                ReportsFolder = "Reports",
                ReportEnabled = false,
                ReportStyleFile = "DefaultReportStyle.xml",
                UrlFormat = UrlFormat.Document,
                Tags = Tags.Category | Tags.Url | Tags.Topic
            };
    }
}
