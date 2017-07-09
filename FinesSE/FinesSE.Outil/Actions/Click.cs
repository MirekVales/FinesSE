using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class Click : IAction
    {
        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.Cast<IEnumerable<IWebElement>>().First());

        public string Invoke(IEnumerable<IWebElement> elements)
        {
            elements.ToList().ForEach(x => x.Click());
            return "";
        }
    }
}
