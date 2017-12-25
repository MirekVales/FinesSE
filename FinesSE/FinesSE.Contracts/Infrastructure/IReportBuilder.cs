using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IReportBuilder
    {
        bool ReportingEnabled { get; }

        void SetTestInfo(Guid id, string name, string description, params string[] tags);

        void AppendScreenshot(Guid id, string path);

        void StartTest(Guid id, string name, string description = null);

        void LogTest(Guid id, LogStatus status, string description = null);

        void EndTest(Guid id, LogStatus status, string description = null);

        void EndTest(Guid id, LogStatus status, Exception exception);

        void AddLog(string log);

        IEnumerable<string> GetTags(IReportable reportable, IExecutionContext context);

        string GetFormattedUrl(IWebDriver driver);

        void Flush();
    }
}