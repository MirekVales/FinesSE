using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinesSE.Core.Parsing;
using System;
using System.Linq;
using FinesSE.Contracts.Exceptions;

namespace FinesSE.UnitTests
{
    [TestClass]
    public class ParserTest
    {
        readonly ParameterParser parser = new ParameterParser();

        public string RandomString => Guid.NewGuid().ToString();

        [TestMethod]
        public void ParsesString()
        {
            var value = RandomString;
            var actual = parser.Parse(new[] { value }, new[] { typeof(string)});
            Assert.AreEqual(value, actual.First());
        }

        [TestMethod]
        public void ParsesInteger()
        {
            var value = DateTime.Now.Millisecond.ToString();
            var actual = parser.Parse(new[] { value }, new[] { typeof(int)});
            Assert.AreEqual(value, actual.First().ToString());
        }

        [TestMethod]
        public void ParsesCustomType()
        {
            var value = new CustomType(RandomString);
            parser.Set<CustomType>(s => new CustomType(s));
            var actual = parser.Parse(new[] { value.Value }, new[] { typeof(CustomType) });
            Assert.AreEqual(value, actual.First());
        }

        public struct CustomType { public string Value; public CustomType(string v) => Value = v; };

        [TestMethod]
        public void FailsOnUndefinedType()
        {
            Assert.ThrowsException<ParserNotFoundException>(()
             => parser.Parse(new[] { "" }, new[] { typeof(CustomType2) }).ToArray());
        }

        public struct CustomType2 { };
    }
}
