using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.IO;
using System.Linq;

namespace FinesSE.Outil.SoapUI
{
    public class GetSoapUICaseResult : IStringAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public string Invoke(string suiteName, string caseName)
        {
            var resultsDirectory = Path.Combine(Context.TempDirectory, "SoapUIResults");
            var result = Directory
                .GetFiles(resultsDirectory, caseName, SearchOption.AllDirectories)
                .SingleOrDefault();
            if (result == null)
                throw new Contracts.Exceptions.FileNotFoundException($"Result for {suiteName}.{caseName} not found");

            return File.ReadAllText(result);
        }
    }
}
