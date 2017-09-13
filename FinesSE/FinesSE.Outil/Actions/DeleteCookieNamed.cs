using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class DeleteCookieNamed : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield break;
        }

        public void Invoke(params object[] parameters)
            => Context
            .Driver
            .Manage()
            .Cookies
            .DeleteCookieNamed(parameters.First() as string);
    }
}
