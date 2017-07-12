using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Type : IVoidAction
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
            yield return typeof(string);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.First() as IEnumerable<IWebElement>, parameters.Last() as string);

        public void Invoke(IEnumerable<IWebElement> elements, string keys)
            => elements.ToList().ForEach(x => x.SendKeys(keys));
    }
}
