using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace FinesSE.Outil.Assertions
{
    public class XPathElementValueNotEquals : IVoidAction, IReportable
    {
        public string Name { get; } = "XPathElementValueNotEquals";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public void Invoke(string xmlDocument, string xpath, string expectedValue)
        {
            using (var reader = new MemoryStream(Encoding.UTF8.GetBytes(xmlDocument)))
            {
                try
                {
                    var navigation = new XPathDocument(reader).CreateNavigator();
                    var value = navigation.SelectSingleNode(xpath).Value;

                    if (value == expectedValue)
                        throw new AssertionException("Different than " + expectedValue, value, WebDrivers.Default);
                }
                catch (XmlException e)
                {
                    throw new XmlParseException(xmlDocument, e.Message);
                }
            }
        }
    }
}