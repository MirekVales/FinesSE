using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_SetEnvelope : IVoidAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public void Invoke(string envelopeId, string envelope)
            => SoapClient.SetEnvelope(envelopeId, envelope);
    }
}
