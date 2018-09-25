using System;
using System.Net;
using System.Text;

namespace FinesSE.Soap.Infrastructure
{
    public class SoapResponse
    {
        public string Id { get; }
        public string Body { get; }
        public WebHeaderCollection Headers { get; }
        public TimeSpan Duration { get; }
        public long SizeInBytes { get; }
        public HttpStatusCode StatusCode { get; }
        public string StatusDescription { get; }
        public bool IsSuccess => (int)StatusCode < 400;

        public SoapResponse(
            string id,
            string body,
            WebHeaderCollection headers,
            TimeSpan duration,
            HttpStatusCode statusCode,
            string status)
        {
            Id = id;
            Body = body;
            Headers = headers;
            Duration = duration;
            SizeInBytes = Encoding.UTF8.GetByteCount(body);
            StatusCode = statusCode;
            StatusDescription = status;
        }
    }
}
