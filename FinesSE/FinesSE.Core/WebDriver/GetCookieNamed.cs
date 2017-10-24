using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;

namespace FinesSE.Outil.Actions
{
    public class GetCookieNamed : IStringAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Context
            .Driver
            .GetCookieNamed(parameters[0] as string)
            .ToString();
    }
}