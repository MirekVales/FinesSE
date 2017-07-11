using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinesSE.UnitTests
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void Deserializes()
        {
            var configurationProvider = new ConfigurationProvider(GetConfiguration());
            Assert.IsTrue(configurationProvider.ConfigurationFound);

            var config = configurationProvider.Get<CustomConfiguration>(null);
            Assert.AreEqual("Test", config.PropertyA);
            Assert.AreEqual(null, config.PropertyB);
            Assert.AreEqual(1111, config.PropertyC);
        }

        private class CustomConfiguration : IConfigurationKeys
        {
            public string PropertyA { get; set; }
            public string PropertyB { get; set; }
            public int PropertyC { get; set; }
        }

        private string GetConfiguration()
            => @"---
PropertyA: Test
PropertyC: 1111
PropertyD: 2222
";
    }
}
