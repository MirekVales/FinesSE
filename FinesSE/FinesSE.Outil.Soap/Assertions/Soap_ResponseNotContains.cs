using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System.Collections.Generic;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_ResponseNotContains : IStringAction, IReportable
    {
        public SoapClient SoapClient { get; set; }

        public string Name => "Soap response should not contain";

        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public string Invoke(string requiredContent, string responseId = null)
        {
            var response = SoapClient.GetResponseContent(responseId);
            if (response.ToString().Contains(requiredContent))
                throw new AssertionException(
                    $"Response should not contain {requiredContent}",
                    $"Response contains",
                    WebDrivers.Default);

            return "true";
        }
    }
}
