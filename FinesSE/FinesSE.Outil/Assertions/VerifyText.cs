using System;
using System.Collections.Generic;
using FinesSE.Contracts;
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
            yield return typeof(LocatedElements);
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as LocatedElements, parameters.Last() as string);

        public string Invoke(LocatedElements elements, string pattern)
        {
            foreach (var element in elements.Elements)
            {
                if (!Regex.IsMatch(element.Text, pattern))
                    throw new AssertionException(pattern, element.Text, WebDrivers.Default);
            }

            if (!elements.Elements.Any())
                throw new Exception("message:<<No element found>>");

            return "";
        }
    }
}
