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
            yield return typeof(IWebElement);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.Cast<IWebElement>().First());

        public string Invoke(IWebElement elements)
            => elements.Text;
    }
}
