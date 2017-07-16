using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Open : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(string);
        }
        
        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<string>().First());

        public void Invoke(string url)
            => Context.Driver.Navigate().GoToUrl(url);
    }
}
