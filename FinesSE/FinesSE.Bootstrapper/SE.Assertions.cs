using FinesSE.Outil.Assertions;
using FinesSE.Outil.Soap.Assertions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void Contains(string needle, string haystack)
            => p.InvokeVoid<Contains>(needle, haystack);

        public void Exists(string locator)
            => p.InvokeVoid<Exists>(locator);

        public void NotContains(string needle, string haystack)
            => p.InvokeVoid<NotContains>(needle, haystack);

        public void NotExists(string locator)
            => p.InvokeVoid<NotExists>(locator);

        public void VerifyEquality(string expected, string actual)
            => p.InvokeVoid<VerifyEquality>(expected, actual);

        public void VerifyText(string locator, string pattern)
            => p.InvokeVoid<VerifyText>(locator, pattern);

        public void Equals(string first, string second)
            => p.InvokeVoid<Equals>(first, second);

        public void NotEquals(string first, string second)
            => p.InvokeVoid<NotEquals>(first, second);

        public void GreaterThan(string first, string second)
            => p.InvokeVoid<GreaterThan>(first, second);

        public void GreaterThanOrEqual(string first, string second)
            => p.InvokeVoid<GreaterThanOrEqual>(first, second);

        public void RegexMatches(string value, string regexPattern)
            => p.InvokeVoid<RegexMatches>(value, regexPattern);

        public void SmallerThan(string first, string second)
            => p.InvokeVoid<SmallerThan>(first, second);

        public void SmallerThanOrEqual(string first, string second)
            => p.InvokeVoid<SmallerThanOrEqual>(first, second);

        public void Soap_CheckSensitiveInformationDisclosure()
            => p.InvokeVoid<Soap_CheckSensitiveInformationDisclosure>();

        public void Soap_DurationLessThan(string duration, string responseId)
            => p.InvokeVoid<Soap_DurationLessThan>(duration, responseId);

        public void Soap_ResponseContains(string requestedPart, string responseId)
            => p.InvokeVoid<Soap_ResponseContains>(requestedPart, responseId);

        public void Soap_ResponseNotContains(string requestedPart, string responseId)
            => p.InvokeVoid<Soap_ResponseNotContains>(requestedPart, responseId);

        public void Soap_XPathElementExists(string xPathExpression, string responseId)
            => p.InvokeVoid<Soap_XPathElementExists>(xPathExpression, responseId);

        public void Soap_XPathElementValueEquals(string xPathExpression, string expectedValue, string responseId)
            => p.InvokeVoid<Soap_XPathElementValueEquals>(xPathExpression, expectedValue, responseId);

        public void XPathElementValueEquals(string xml, string xpath, string expected)
            => p.InvokeVoid<XPathElementValueEquals>(xml, xpath, expected);

        public void XPathElementValueNotEquals(string xml, string xpath, string expected)
            => p.InvokeVoid<XPathElementValueNotEquals>(xml, xpath, expected);
    }
}