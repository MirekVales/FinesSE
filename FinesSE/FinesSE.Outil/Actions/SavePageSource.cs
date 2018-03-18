using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.IO;

namespace FinesSE.Outil.Actions
{
    public class SavePageSource : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string filePath)
            => File.WriteAllText(filePath, Context.Driver.PageSource);
    }
}