using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_SetCredentials : IVoidAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public void Invoke(string username, string passphrase, string domain = null)
            => SoapClient.SetCredentials(username, passphrase, domain);
    }
}
