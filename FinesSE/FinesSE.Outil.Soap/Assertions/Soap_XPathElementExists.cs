using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_XPathElementExists : IVoidAction, IReportable
    {
        public SoapClient SoapClient { get; set; }
        public string Name => "Element can be found using XPath";
        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory};

        [EntryPoint]
        public void Invoke(string xpathExpression, string responseId = null)
        {
            var response = XDocument.Parse(SoapClient.GetResponseContent(responseId));
            if (response.XPathSelectElements(xpathExpression).Any())
                return;

            throw new AssertionException("Element exists", "Element does not exist", WebDrivers.Default);
        }
    }
}
