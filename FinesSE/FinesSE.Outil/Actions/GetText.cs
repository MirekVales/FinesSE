using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class GetText : IAction
    {
        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.Cast<IEnumerable<IWebElement>>().First());

        public string Invoke(IEnumerable<IWebElement> elements)
            => elements.First().Text;
    }
}
