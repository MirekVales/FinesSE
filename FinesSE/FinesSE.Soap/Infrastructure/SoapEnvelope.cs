using System;
using System.Text.RegularExpressions;
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

        public XmlDocument Get(string message)
        {
            var envelope = new XmlDocument();
            envelope.LoadXml(ShapeMessage(Body, message));
            return envelope;
        }

        public string ShapeMessage(string body, string message)
        {
            var content = body.Replace("${=content}", message);

            Match match;
            while ((match = Regex.Match(content, @"\$\{=guid\}")).Success)
            {
                content = content.Remove(match.Index, match.Length);
                content = content.Insert(match.Index, Guid.NewGuid().ToString());
            }

            return content;
        }
    }
}
