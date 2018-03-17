using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IExecutionContext : IDisposable
    {
        string TopicId { get; }
        IWebDriver Driver { get; }
        IEnumerable<WebDrivers> Drivers { get; }
        IConfigurationProvider ConfigurationProvider { get; }

        void SetTopicId(string id);
        void SetBrowser(WebDrivers drivers);

        void AddWorkflowBranch(BranchType branchType);
        bool IsActionIgnored();

        string TempDirectory { get; }
    }
}