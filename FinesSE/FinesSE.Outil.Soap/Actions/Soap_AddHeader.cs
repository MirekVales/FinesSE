using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_AddHeader : IVoidAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public void Invoke(string name, string value)
            => SoapClient.AddHeader(name, value);
    }
}
