using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class SetBrowser : IAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(WebDrivers);
        }

        public string Invoke(params object[] parameters)
            => Invoke((WebDrivers)parameters.First());

        public string Invoke(WebDrivers drivers)
        {
            Context.SetBrowser(drivers);
            return $"Browser set to '{drivers}'";
        }
    }
}
