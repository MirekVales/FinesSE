using FinesSE.Contracts.Invokable;
using FinesSE.Soap.Infrastructure;

namespace FinesSE.Outil.Soap.Actions
{
    public class Soap_GetResponse : IStringAction
    {
        public SoapClient SoapClient { get; set; }

        [EntryPoint]
        public string Invoke(string responseId = null)
            => SoapClient.GetResponseContent(responseId).ToString();
    }
}
