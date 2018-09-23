using System;
using System.Net;

namespace FinesSE.Soap.Infrastructure
{
    public class SoapResponse
    {
        public string Id { get; }
        public string Body { get; }
        public WebHeaderCollection Headers { get; }
        public TimeSpan Duration { get; }

        public SoapResponse(string id, string body, WebHeaderCollection headers, TimeSpan duration)
        {
            Id = id;
            Body = body;
            Headers = headers;
            Duration = duration;
        }
    }
}
