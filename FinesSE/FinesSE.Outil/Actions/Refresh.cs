using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;

namespace FinesSE.Outil.Actions
{
    public class Refresh : IVoidAction
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield break;
        }

        public void Invoke(params object[] parameters)
            => Invoke();

        public void Invoke()
            => DriverProvider.Get().Navigate().Refresh();
    }
}
