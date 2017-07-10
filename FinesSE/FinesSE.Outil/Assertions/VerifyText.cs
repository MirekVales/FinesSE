using System;
using System.Collections.Generic;
using FinesSE.Contracts;
using OpenQA.Selenium;
using System.Linq;
using System.Text.RegularExpressions;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Assertions
{
    public class VerifyText : IAction
    {
        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as IEnumerable<IWebElement>, parameters.Last() as string);

        public string Invoke(IEnumerable<IWebElement> elements, string pattern)
        {
            foreach (var element in elements)
            {
                if (!Regex.IsMatch(element.Text, pattern))
                    throw new AssertionException(pattern, element.Text, WebDrivers.Default);
            }

            if (!elements.Any())
                throw new Exception("message:<<No element found>>");

            return "";
        }
    }
}
