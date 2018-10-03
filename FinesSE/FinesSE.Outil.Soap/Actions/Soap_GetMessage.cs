using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_GetMessage : IStringAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public string Invoke(string envelopeId, string messageId)
            => SoapClient.CreateData(envelopeId, messageId).OuterXml;
    }
}
