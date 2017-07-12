using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class Click : IVoidAction
    {
        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<IEnumerable<IWebElement>>().First());

        public void Invoke(IEnumerable<IWebElement> elements)
            => elements.ToList().ForEach(x => x.Click());
    }
}
