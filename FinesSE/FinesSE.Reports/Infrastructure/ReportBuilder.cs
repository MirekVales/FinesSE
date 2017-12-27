using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using FinesSE.Core.Environment;
using log4net;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using LogStatus = FinesSE.Contracts.Infrastructure.LogStatus;

namespace FinesSE.Reports.Infrastructure
{
    public class ReportBuilder : IReportBuilder
    {
        readonly ExtentReports report;
        readonly ReportsConfiguration configuration;
        readonly ILog log;

        readonly Dictionary<Guid, ExtentTest> tests;

        public bool ReportingEnabled => configuration.ReportEnabled;


        public ReportBuilder(IConfigurationProvider provider, ILog log)
        {
            configuration = provider.Get(ReportsConfiguration.Default);
            this.log = log;

            tests = new Dictionary<Guid, ExtentTest>();

            var reportPath = Path.Combine(
                configuration
                    .ReportsFolder
                    .GetRootedPath()
                    .EnsureDirectoryExistence(),
                $"Report.html").GetRootedPath();
            var stylePath = configuration.ReportStyleFile.GetRootedPath();

            log.Info($"Report style taken from {stylePath}");
            log.Info($"Report being rendered to {reportPath}");

            report = new ExtentReports(reportPath, false, DisplayOrder.NewestFirst);
            report.LoadConfig(stylePath);

            AppendEnvironmentInfo();
        }

        void AppendEnvironmentInfo()
        {
            foreach (var (key, value) in EnvironmentInfo.GetInfo())
                report.AddSystemInfo(key, value);
        }

        public void SetTestInfo(Guid id, string name, string description, params string[] tags)
        {
            tests[id].GetTest().Name = name;
            tests[id].GetTest().Description = description;
            if (tags != null && tags.Length > 0)
                tests[id].AssignCategory(tags);
        }

        public void AppendScreenshot(Guid id, string path)
            => tests[id].Log(
                RelevantCodes.ExtentReports.LogStatus.Info,
                tests[id].AddBase64ScreenCapture(GetBase64(path)));

        string GetBase64(string imagePath)
        {
            using (var image = new Bitmap(imagePath))
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                byte[] imageBytes = stream.ToArray();
                return "data:image/png;base64," + System.Convert.ToBase64String(imageBytes);
            }
        }

        public void AddLog(string log)
            => report.AddTestRunnerOutput(log);

        public void StartTest(Guid id, string name, string description = null)
            => tests.Add(id, report.StartTest(name, description));

        public void LogTest(Guid id, LogStatus status, string description = null)
            => tests[id].Log(Convert(status), description);

        public void EndTest(Guid id, LogStatus status, string details)
        {
            tests[id].Log(Convert(status), details);
            report.EndTest(tests[id]);
            Flush();
        }

        public void EndTest(Guid id, LogStatus status, Exception e)
        {
            tests[id].Log(Convert(status), e);
            report.EndTest(tests[id]);
            Flush();
        }

        RelevantCodes.ExtentReports.LogStatus Convert(LogStatus status)
            => (RelevantCodes.ExtentReports.LogStatus)Enum
            .Parse(typeof(RelevantCodes.ExtentReports.LogStatus), status.ToString());

        public IEnumerable<string> GetTags(IReportable reportable, IExecutionContext context)
        {
            if (configuration.Tags.HasFlag(Tags.Category))
                foreach (var category in reportable.Category)
                    yield return category;
            if (configuration.Tags.HasFlag(Tags.Url))
                yield return GetFormattedUrl(context.Driver);
            if (configuration.Tags.HasFlag(Tags.Topic))
                yield return context.TopicId;
        }

        public string GetFormattedUrl(IWebDriver driver)
        {
            var url = driver.Url;

            switch (configuration.UrlFormat)
            {
                case UrlFormat.Hidden:
                    return null;
                case UrlFormat.Document:
                    return Path.GetFileName(url);
                case UrlFormat.FullPath:
                    return url;
                default:
                    return null;
            }
        }

        public void Flush()
        {
            log.Info("Flushing report");
            report.Flush();
        }
    }
}