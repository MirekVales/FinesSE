using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using FinesSE.Expressions.Contracts;
using log4net;
using System.IO;
using System.Linq;

namespace FinesSE.Outil.Expressions
{
    public class Import : IVoidAction
    {
        public ILog Log { get; set; }
        public IExpressionEngine ExpressionEngine { get; set; }

        [EntryPoint]
        public void Invoke(string path)
        {
            var scriptPath = path.GetRootedPath();
            Log.Info($"Importing from {scriptPath}");

            File.ReadAllLines(scriptPath)
                .Select((line, index) => $"Line {index + 1} returned {ExpressionEngine.Execute(line.Trim())}")
                .ToList()
                .ForEach(Log.Info);
        }
    }
}
