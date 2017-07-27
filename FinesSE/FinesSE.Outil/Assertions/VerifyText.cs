using System;
using System.Collections.Generic;
using FinesSE.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Assertions
{
    public class VerifyText : IVoidAction
    {
        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
            yield return typeof(string);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.First() as LocatedElements, parameters.Last() as string);

        public void Invoke(LocatedElements elements, string pattern)
            => elements
                .ConstraintCount(c => c > 0)
                .Elements
                .ForEach(e => VerifyElement(pattern, e));

        private static void VerifyElement(string pattern, OpenQA.Selenium.IWebElement element)
        {
            if (!Regex.IsMatch(element.Text, pattern))
                throw new AssertionException(pattern, element.Text, WebDrivers.Default);
        }
    }
}
