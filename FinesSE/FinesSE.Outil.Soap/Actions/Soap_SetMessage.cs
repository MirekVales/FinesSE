using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_SetMessage : IVoidAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public void Invoke(string messageId, string message)
            => SoapClient.SetMessage(messageId, message);
    }
}
