using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_SetEncoding : IVoidAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public void Invoke(string encoding)
            => SoapClient.Set(encoding);
    }
}
