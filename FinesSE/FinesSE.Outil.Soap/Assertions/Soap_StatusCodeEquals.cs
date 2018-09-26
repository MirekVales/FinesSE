using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_StatusCodeEquals : IVoidAction, IReportable
    {
        public SoapClient SoapClient { get; set; }

        public string Name => "Soap response's status code must equal";

        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string statusCodes, string responseId)
        {
            var response = SoapClient.GetResponse(responseId);
            var statusCode = ((int)response.StatusCode).ToString();
            if (!statusCodes.Split(',').Contains(statusCode))
                throw new AssertionException(
                    $"Status code must equal {string.Join(",", statusCodes)}",
                    $"Status code was {statusCode}",
                    WebDrivers.Default);
        }
    }
}
