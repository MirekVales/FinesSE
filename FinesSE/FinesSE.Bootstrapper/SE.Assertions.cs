using FinesSE.Outil.Assertions;
using FinesSE.Outil.Soap.Assertions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public string Contains(string needle, string haystack)
            => p.Invoke<Contains>(needle, haystack);

        public string Exists(string locator)
            => p.Invoke<Exists>(locator);

        public string NotContains(string needle, string haystack)
            => p.Invoke<NotContains>(needle, haystack);

        public string NotExists(string locator)
            => p.Invoke<NotExists>(locator);

        public string VerifyEquality(string expected, string actual)
            => p.Invoke<VerifyEquality>(expected, actual);

        public string VerifyText(string locator, string pattern)
            => p.Invoke<VerifyText>(locator, pattern);

        public string Equals(string first, string second)
            => p.Invoke<Equals>(first, second);

        public string NotEquals(string first, string second)
            => p.Invoke<NotEquals>(first, second);

        public string GreaterThan(string first, string second)
            => p.Invoke<GreaterThan>(first, second);

        public string GreaterThanOrEqual(string first, string second)
            => p.Invoke<GreaterThanOrEqual>(first, second);

        public string RegexMatches(string value, string regexPattern)
            => p.Invoke<RegexMatches>(value, regexPattern);

        public string SmallerThan(string first, string second)
            => p.Invoke<SmallerThan>(first, second);

        public string SmallerThanOrEqual(string first, string second)
            => p.Invoke<SmallerThanOrEqual>(first, second);

        public string Soap_CheckSensitiveInformationDisclosure(string responseId)
            => p.Invoke<Soap_CheckSensitiveInformationDisclosure>(responseId);

        public string Soap_DurationLessThan(string duration, string responseId)
            => p.Invoke<Soap_DurationLessThan>(duration, responseId);

        public string Soap_ResponseIsSuccess(string responseId = null)
            => p.Invoke<Soap_ResponseIsSuccess>(responseId);

        public bool Soap_ResponseContains(string requestedPart, string responseId)
            => p.InvokeBool<Soap_ResponseContains>(requestedPart, responseId);

        public string Soap_ResponseNotContains(string requestedPart, string responseId)
            => p.Invoke<Soap_ResponseNotContains>(requestedPart, responseId);

        public string Soap_StatusCodeEquals(string statusCode, string responseId)
            => p.Invoke<Soap_StatusCodeEquals>(statusCode, responseId);

        public string Soap_XPathElementExists(string xPathExpression, string responseId)
            => p.Invoke<Soap_XPathElementExists>(xPathExpression, responseId);

        public string Soap_XPathElementValueEquals(string xPathExpression, string expectedValue, string responseId)
            => p.Invoke<Soap_XPathElementValueEquals>(xPathExpression, expectedValue, responseId);

        public string XPathElementValueEquals(string xml, string xpath, string expected)
            => p.Invoke<XPathElementValueEquals>(xml, xpath, expected);

        public string XPathElementValueNotEquals(string xml, string xpath, string expected)
            => p.Invoke<XPathElementValueNotEquals>(xml, xpath, expected);
    }
}