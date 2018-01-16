using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.SoapUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.Reports
{
    public class RunSoapUITests : IVoidAction
    {
        public IReportBuilder ReportBuilder { get; set; }

        public IConfigurationProvider Provider { get; set; }

        [EntryPoint]
        public void Invoke(string pathToTests, string suiteName)
        {
            var configuration = Provider.Get(SoapUIRunnerConfiguration.Default);

            ValidateConfiguration(configuration);

            ProcessStartInfo startInfo = GetStartInfo(configuration, pathToTests, suiteName);

            var start = DateTime.Now;
            var result = RunTest(startInfo);
            var end = DateTime.Now;

            foreach (var (name, timeTaken, status) in ParseTestCases(result))
            {
                var id = Guid.NewGuid();
                ReportBuilder.StartTest(id, name);
                ReportBuilder.SetTestTimeInfo(id, start, end);
                ReportBuilder.EndTest(id, status, "Elapsed time: " + timeTaken);
            }
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

        IEnumerable<(string name, string timeTaken, LogStatus status)> ParseTestCases(string consoleOutput)
        {
            const string RegexPattern = @"Finished running SoapUI testcase \[(.+?)\], time taken: ([0-9]+)ms, status: (.+?)\b";

            LogStatus isSuccess(string stringValue)
                => stringValue.ToLower() == "finished" ? LogStatus.Pass : LogStatus.Fail;

            foreach (Match match in Regex.Matches(consoleOutput, RegexPattern))
            {
                yield return (match.Groups[1].Value, match.Groups[2].Value, isSuccess(match.Groups[3].Value));
            }
        }

        string RunTest(ProcessStartInfo startInfo)
        {
            using (var process = Process.Start(startInfo))
            {
                var output = process.StandardOutput.ReadToEnd();
                var errorOutput = process.StandardError.ReadToEnd();

                process.WaitForExit();

                return string.IsNullOrWhiteSpace(errorOutput)
                    ? output
                    : errorOutput;
            }
        }
    }
}
