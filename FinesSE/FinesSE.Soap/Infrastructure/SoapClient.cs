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
        public string UserAgent { get; private set; }

        readonly Dictionary<string, SoapEnvelope> envelopes;
        readonly Dictionary<string, string> messages;
        readonly Dictionary<string, SoapResponse> responses;

        string lastResponseId;

        NetworkCredential credentials;
        readonly WebHeaderCollection headers;
        Version version;

        public SoapClient()
        {
            envelopes = new Dictionary<string, SoapEnvelope>();
            messages = new Dictionary<string, string>();
            responses = new Dictionary<string, SoapResponse>();
            Encoding = "utf-8";

            version = HttpVersion.Version11;
            headers = new WebHeaderCollection
            {
                @"SOAP:Action"
            };
        }

        public XDocument GetResponseContent(string responseId)
            => XDocument.Parse(GetResponse(responseId).Body);

        public SoapResponse GetResponse(string responseId)
            => responses[responseId ?? lastResponseId];

        public void SetCredentials(string username, string passphrase, string domain = null)
            => credentials = new NetworkCredential(username, passphrase, domain);

        public void SetEncoding(string encoding)
            => Encoding = encoding;

        public void SetUserAgent(string userAgent)
            => UserAgent = userAgent;

        public void SetHttpVersion(Version version)
            => this.version = version;

        public void AddHeader(string name, string value)
            => headers.Add(name, value);

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
            try
            {
                using (var response = request.GetResponse())
                {
                    string content;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                        content = reader.ReadToEnd();

                    var statusCode = HttpStatusCode.OK;
                    var statusDescription = "";
                    if (response is HttpWebResponse httpWebResponse)
                    {
                        statusCode = httpWebResponse.StatusCode;
                        statusDescription = httpWebResponse.StatusDescription;
                    }

                    responses[messageId] = new SoapResponse(
                        messageId,
                        content,
                        response.Headers,
                        stopwatch.Elapsed,
                        statusCode,
                        statusDescription);
                }
            }
            catch (WebException exception)
            {
                if (exception.Response is HttpWebResponse response)
                {
                    HttpStatusCode statusCode = response.StatusCode;
                    var statusDescription = response.StatusDescription;

                    responses[messageId] = new SoapResponse(
                        messageId,
                        "",
                        response.Headers,
                        stopwatch.Elapsed,
                        statusCode,
                        statusDescription);
                }
                else
                    throw exception;
            }

            lastResponseId = messageId;

            return true;
        }

        public HttpWebRequest CreateWebRequest(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ProtocolVersion = version;
            webRequest.Headers = headers;
            webRequest.ContentType = $"text/xml;charset=\"{Encoding}\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.UserAgent = UserAgent;
            webRequest.Credentials = credentials;
            return webRequest;
        }

        public void Dispose()
        {
        }
    }
}
