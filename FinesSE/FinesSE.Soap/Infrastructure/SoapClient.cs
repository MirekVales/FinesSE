using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Diagnostics;

namespace FinesSE.Soap.Infrastructure
{
    public class SoapClient : IDisposable
    {
        public string Encoding { get; private set; }

        readonly Dictionary<string, SoapEnvelope> envelopes;
        readonly Dictionary<string, string> messages;
        readonly Dictionary<string, SoapResponse> responses;

        string lastResponseId;

        NetworkCredential credentials;

        public SoapClient()
        {
            envelopes = new Dictionary<string, SoapEnvelope>();
            messages = new Dictionary<string, string>();
            responses = new Dictionary<string, SoapResponse>();
            Encoding = "utf-8";
        }

        public XDocument GetResponseContent(string responseId)
            => XDocument.Parse(GetResponse(responseId).Body);

        public SoapResponse GetResponse(string responseId)
            => responses[responseId == null ? lastResponseId : responseId];

        public void SetCredentials(string username, string passphrase, string domain = null)
            => credentials = new NetworkCredential(username, passphrase, domain);

        public void Set(string encoding)
            => Encoding = encoding;

        public void SetEnvelope(string envelopeId, string envelope)
            => envelopes[envelopeId] = new SoapEnvelope(envelopeId, envelope);

        public void SetMessage(string messageId, string message)
            => messages[messageId] = message;

        public bool Invoke(string url, string envelopeId, string messageId)
        {
            var request = CreateWebRequest(url);
            var data = envelopes[envelopeId].Get(messages[messageId]);
            
            using (var stream = request.GetRequestStream())
                data.Save(stream);

            var stopwatch = Stopwatch.StartNew();
            using (var response = request.GetResponse())
            {
                string content;
                using (var rd = new StreamReader(response.GetResponseStream()))
                    content = rd.ReadToEnd();

                responses[messageId] = new SoapResponse(
                    messageId,
                    content,
                    response.Headers,
                    stopwatch.Elapsed);
            }

            lastResponseId = messageId;

            return true;
        }

        public HttpWebRequest CreateWebRequest(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = $"text/xml;charset=\"{Encoding}\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Credentials = credentials;
            return webRequest;
        }

        public void Dispose()
        {
        }
    }
}
