using FinesSE.Contracts;
using System.Text.RegularExpressions;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Assertions
{
    public class VerifyText : IVoidAction
    {
        [EntryPoint]
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
