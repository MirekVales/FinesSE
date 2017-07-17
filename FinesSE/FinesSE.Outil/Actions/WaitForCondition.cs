using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class WaitForCondition : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(string);
            yield return typeof(int);
        }

        public void Invoke(params object[] parameters)
        {
            var script = parameters.First() as string;
            var timeoutMs = (int)parameters.Last();
            Predicate<string> finished =
                s => string.Equals(s, "true", StringComparison.InvariantCultureIgnoreCase);

            var timeout = DateTime.Now.AddMilliseconds(timeoutMs);
            while (DateTime.Now < timeout && !finished(Context.Driver.ExecuteScript(script) + ""))
            { }
        }
    }
}
