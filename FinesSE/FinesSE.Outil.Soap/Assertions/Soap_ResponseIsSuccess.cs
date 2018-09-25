using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System.Collections.Generic;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_ResponseIsSuccess : IVoidAction, IReportable
    {
        public SoapClient SoapClient { get; set; }

        public string Name => "Soap response has a status code lower than 400";

        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string responseId, string maxDuration)
        {
            var response = SoapClient.GetResponse(responseId);
            var statusCode = (int)response.StatusCode;
            if (statusCode >= 400)
                throw new AssertionException(
                    $"Status code must be smaller than 400",
                    $"Status code was {statusCode}",
                    WebDrivers.Default);
        }
    }
}
