using FinesSE.Soap.Infrastructure.DummyData;
using System.Xml;

namespace FinesSE.Soap.Infrastructure
{
    public class SoapEnvelope
    {
        public string Id { get; }
        public string Body { get; }

        public SoapEnvelope(string id, string envelope)
        {
            Id = id;
            Body = envelope;
        }

        public XmlDocument Get(string message, IDummyDataProcessor dataProcessor)
        {
            var envelope = new XmlDocument();
            envelope.LoadXml(ShapeMessage(Body, message, dataProcessor));
            return envelope;
        }

        public string ShapeMessage(string body, string message, IDummyDataProcessor dataProcessor)
        {
            var content = body.Replace("${=content}", message);

            return dataProcessor.ProcessMessage(content);
        }
    }
}
