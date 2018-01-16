﻿using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.SoapUI;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.Reports
{
    public class RunSoapUITests : IStringAction
    {
        public IReportBuilder ReportBuilder { get; set; }
        public IConfigurationProvider Provider { get; set; }
        public ILog Log { get; set; }

        [EntryPoint]
        public string Invoke(string pathToTests, string suiteName)
        {
            var configuration = Provider.Get(SoapUIRunnerConfiguration.Default);

            ValidateConfiguration(configuration);

            ProcessStartInfo startInfo = GetStartInfo(configuration, pathToTests, suiteName);

            var start = DateTime.Now;
            var result = RunTest(startInfo);

            var results = ParseTestCases(result).ToList();

            foreach (var (name, timeTaken, status) in results)
            {
                var id = Guid.NewGuid();
                ReportBuilder.StartTest(id, name);
                ReportBuilder.SetTestTimeInfo(id, start, start.AddMilliseconds(timeTaken));
                ReportBuilder.EndTest(id, status, $"{timeTaken} ms");
            }

            return FormatResultMessage(results);
        }

        string FormatResultMessage(IEnumerable<(string name, int msTaken, LogStatus status)> results)
        {
            var builder = new StringBuilder();
            foreach (var (name, msTaken, status) in results.Where(r => r.status == LogStatus.Fail))
                builder.AppendLine($"X Test case {name} failed after {msTaken} ms");
            foreach (var (name, msTaken, status) in results.Where(r => r.status == LogStatus.Pass))
                builder.AppendLine($"_ Test case {name} passed after {msTaken} ms");
            return builder.ToString();
        }

        void ValidateConfiguration(SoapUIRunnerConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration.RunnerPath) || !File.Exists(configuration.RunnerPath))
                throw new Contracts.Exceptions.FileNotFoundException($"Runner not found at {configuration.RunnerPath}");
        }

        ProcessStartInfo GetStartInfo(SoapUIRunnerConfiguration configuration, string pathToTests, string suiteName)
        {
            return new ProcessStartInfo()
            {
                Arguments = $"-r -s \"{suiteName}\" \"{pathToTests}\" -I",
                CreateNoWindow = true,
                FileName = configuration.RunnerPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
        }

        IEnumerable<(string name, int msTaken, LogStatus status)> ParseTestCases(string consoleOutput)
        {
            const string RegexPattern = @"Finished running SoapUI testcase \[(.+?)\], time taken: ([0-9]+)ms, status: (.+?)\b";

            LogStatus isSuccess(string stringValue)
                => stringValue.ToLower() == "finished" ? LogStatus.Pass : LogStatus.Fail;

            foreach (Match match in Regex.Matches(consoleOutput, RegexPattern))
            {
                yield return (match.Groups[1].Value, int.Parse(match.Groups[2].Value), isSuccess(match.Groups[3].Value));
            }
        }

        string RunTest(ProcessStartInfo startInfo)
        {
            Log.Debug($"Starting process {startInfo.FileName} {startInfo.Arguments}");
            using (var process = Process.Start(startInfo))
            {
                var output = process.StandardOutput.ReadToEnd();
                var errorOutput = process.StandardError.ReadToEnd();

                process.WaitForExit();
                Log.Debug("Processed exited");

                return string.IsNullOrWhiteSpace(errorOutput)
                    ? output
                    : errorOutput;
            }
        }
    }
}
