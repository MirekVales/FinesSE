using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Outil.Actions
{
    public class SetBrowser : IStringAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public string Invoke(WebDrivers drivers)
        {
            Context.SetBrowser(drivers);
            return $"Browser set to '{drivers}'";
        }
    }
}
