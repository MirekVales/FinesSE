using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;

namespace FinesSE.Outil.Actions
{
    public class GetCookies : IAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield break;
        }

        public string Invoke(params object[] parameters)
            => string.Join(";", Context.Driver.GetCookies());
    }
}
