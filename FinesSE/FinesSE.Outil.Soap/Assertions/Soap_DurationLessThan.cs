using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;
using System;
using System.Collections.Generic;

namespace FinesSE.Outil.Soap.Assertions
{
    public class Soap_DurationLessThan : IVoidAction, IReportable
    {
        public SoapClient SoapClient { get; set; }

        public string Name => "Soap call duration is less than";

        public string Description { get; }

        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string maxDuration, string responseId)
        {
            var response = SoapClient.GetResponse(responseId);
            if (response.Duration > TimeSpan.Parse(maxDuration))
                throw new AssertionException(
                    $"Duration is smaller than {maxDuration}",
                    $"Duration was {response.Duration}",
                    WebDrivers.Default);
        }
    }
}
