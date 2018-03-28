using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.SoapUI
{
    public class GetSoapUICaseResponse : IStringAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public string Invoke(string suiteName, string caseName)
        {
            var resultsDirectory = Path.Combine(
                Context.TempDirectory,
                "SoapUIResults",
                suiteName.MakeFileNameSafe('_', ' '));
            var result = Directory
                .GetFiles(
                    resultsDirectory,
                    caseName.MakeFileNameSafe('_',' ') + "-*",
                    SearchOption.AllDirectories)
                .FirstOrDefault();
            if (result == null)
                throw new Contracts.Exceptions.FileNotFoundException($"Result for {suiteName}.{caseName} not found");

            return ExtractResponse(File.ReadAllText(result));
        }

        string ExtractResponse(string content)
        {
            var match = Regex.Match(
                content,
                @"(--- Response ---)([^\<]+)(\<(.|\s)+\>)",
                RegexOptions.Multiline);
            return match.Success ? match.Groups[3].Value : "Not available";
        }
    }
}
