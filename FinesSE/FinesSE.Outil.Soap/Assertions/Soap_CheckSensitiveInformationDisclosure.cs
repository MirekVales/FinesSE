using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_CheckSensitiveInformationDisclosure : IVoidAction, IReportable
    {
        public SoapClient SoapClient { get; set; }

        public string Name => "Checks whether response does not contain sensitive information";

        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string responseId = null, string xpathExpression = null)
        {
            var response = SoapClient.GetResponse(responseId);

            var incidents = new List<string>();
            if (!string.IsNullOrWhiteSpace(response.Headers["Server"]))
                incidents.Add($"Server header disclosure ({response.Headers["Server"]})");

            if (!string.IsNullOrWhiteSpace(response.Headers["X-Powered-By"]))
                incidents.Add($"X-Powered-By header disclosure ({response.Headers["X-Powered-By"]})");

            if (string.IsNullOrWhiteSpace(response.Headers["X-Frame-Options"])
                || !string.Equals(response.Headers["X-Frame-Options"], "deny", StringComparison.InvariantCultureIgnoreCase))
                incidents.Add($"X-Frame-Options header different than DENY ({response.Headers["X-Frame-Options"]})");

            if (incidents.Any())
                throw new AssertionException(
                    "Response should not contain sensitive information",
                    string.Join(Environment.NewLine, incidents),
                    WebDrivers.Default);
        }
    }
}
