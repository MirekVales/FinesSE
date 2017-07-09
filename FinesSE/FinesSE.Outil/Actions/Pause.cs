using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinesSE.Outil.Actions
{
    public class Pause : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(int);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.Cast<int>().First());

        public string Invoke(int ms)
        {
            Task.Delay(ms).Wait();
            return "";
        }
    }
}
