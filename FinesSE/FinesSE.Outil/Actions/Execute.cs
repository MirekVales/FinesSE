using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class Execute : IStringAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public string Invoke(string script)
            => Context.Driver.ExecuteScript(script) + "";
    }
}
