using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class GetPageSource : IStringAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public string Invoke()
            => Context.Driver.PageSource;
    }
}