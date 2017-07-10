using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class SetBrowser : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(WebDrivers);
        }

        public string Invoke(params object[] parameters)
            => Invoke((WebDrivers)parameters.First());

        public string Invoke(WebDrivers drivers)
        {
            DriverProvider.SetBrowser(drivers);
            return $"Browser set to '{drivers}'";
        }
    }
}
