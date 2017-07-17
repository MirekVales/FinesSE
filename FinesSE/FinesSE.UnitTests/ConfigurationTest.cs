using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Configuration;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FinesSE.UnitTests
{
    [TestClass]
    public class ConfigurationTest
    {
        private readonly ILog mLog = new Mock<ILog>().Object;

        [TestMethod]
        public void Deserializes()
        {
            var configurationProvider = new ConfigurationProvider(mLog, GetConfiguration());
            Assert.IsTrue(configurationProvider.ConfigurationFound);

            var config = configurationProvider.Get<CustomConfiguration>(null);
            Assert.AreEqual("Test", config.PropertyA);
            Assert.AreEqual(null, config.PropertyB);
            Assert.AreEqual(1111, config.PropertyC);
        }

        [TestMethod]
        public void FallbacksToDefaultValues()
        {
            var configurationProvider = new ConfigurationProvider(mLog, "");
            Assert.IsTrue(configurationProvider.ConfigurationFound);

            var fallback = new CustomConfiguration()
            {
                PropertyA = "Test",
                PropertyB = null,
                PropertyC = 1111
            };
            var config = configurationProvider.Get(fallback);
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
Custom:
    PropertyA: Test
    PropertyC: 1111
    PropertyD: 2222
";
    }
}
