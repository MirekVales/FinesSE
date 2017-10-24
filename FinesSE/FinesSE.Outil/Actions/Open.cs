using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class Open : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string url)
            => Context.Driver.Navigate().GoToUrl(url);
    }
}
