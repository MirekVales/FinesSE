using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_Send : IStringAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public string Invoke(string url, string soapAction, string envelopeId, string messageId)
        {
            if (string.IsNullOrWhiteSpace(messageId))
                return SoapClient.Invoke(url, soapAction, envelopeId) + "";
            else
                return SoapClient.Invoke(url, soapAction, envelopeId, messageId) + "";
        }
    }
}