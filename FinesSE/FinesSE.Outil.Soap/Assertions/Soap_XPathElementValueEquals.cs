﻿using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_XPathElementValueEquals : IVoidAction, IReportable
    {
        public SoapClient SoapClient { get; set; }
        public string Name => "Element can be found using XPath";
        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory};

        [EntryPoint]
        public void Invoke(string responseId = null, string xpathExpression = null, string expectedValue = null)
        {
            var response = SoapClient.GetResponseContent(responseId);
            var value = response.XPathSelectElement(xpathExpression).Descendants().First().Value;

            if (value != expectedValue)
                throw new AssertionException(value, expectedValue, WebDrivers.Default);
        }
    }
}