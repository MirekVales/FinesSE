using FinesSE.Contracts;
using System.Text.RegularExpressions;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;

namespace FinesSE.Outil.Assertions
{
    public class VerifyText : IVoidAction, IReportable
    {
        public string Name { get; } = "Verify Text";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(LocatedElements elements, string pattern)
            => elements
                .ConstraintCount(c => c > 0)
                .Elements
                .ForEach(e => VerifyElement(pattern, e));

        void VerifyElement(string pattern, OpenQA.Selenium.IWebElement element)
        {
            if (!Regex.IsMatch(element.Text, pattern))
                throw new AssertionException(pattern, element.Text, WebDrivers.Default);
        }
    }
}
