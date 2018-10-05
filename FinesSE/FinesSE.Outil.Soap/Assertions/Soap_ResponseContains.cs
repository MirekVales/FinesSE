using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System.Collections.Generic;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_ResponseContains : IStringAction, IReportable
    {
        public SoapClient SoapClient { get; set; }
        public string Name => "Soap response should contain";
        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public string Invoke(string requiredContent, string responseId = null)
        {
            var response = SoapClient.GetResponseContent(responseId);
            if (!response.ToString().Contains(requiredContent))
                throw new AssertionException(
                    $"Response should contain {requiredContent}",
                    $"Response does not contain",
                    WebDrivers.Default);

            return "true";
        }
    }
}
