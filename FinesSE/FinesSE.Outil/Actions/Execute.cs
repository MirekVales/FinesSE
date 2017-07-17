using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Execute : IAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Context.Driver.ExecuteScript(parameters.Cast<string>().First()) + "";
    }
}
