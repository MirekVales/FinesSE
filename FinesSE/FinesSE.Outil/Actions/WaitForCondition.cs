using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System;

namespace FinesSE.Outil.Actions
{
    public class WaitForCondition : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string script, int timeoutMs)
        {
            Predicate<string> finished =
                s => string.Equals(s, "true", StringComparison.InvariantCultureIgnoreCase);

            var timeout = DateTime.Now.AddMilliseconds(timeoutMs);
            while (DateTime.Now < timeout && !finished(Context.Driver.ExecuteScript(script) + ""))
            { }
        }
    }
}
