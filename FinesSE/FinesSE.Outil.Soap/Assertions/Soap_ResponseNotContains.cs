using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System.Collections.Generic;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_ResponseNotContains : IVoidAction, IReportable
    {
        public SoapClient SoapClient { get; set; }

        public string Name => "Soap response should not contain";

        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string requiredContent, string responseId)
        {
            var response = SoapClient.GetResponseContent(responseId);
            if (response.ToString().Contains(requiredContent))
                throw new AssertionException(
                    $"Response should not contain {requiredContent}",
                    $"Response contains",
                    WebDrivers.Default);
        }
    }
}
