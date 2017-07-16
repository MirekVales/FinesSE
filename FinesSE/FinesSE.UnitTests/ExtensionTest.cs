using FinesSE.Contracts;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinesSE.UnitTests
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void SplitsMultipleFlags()
        {
            var values = WebDrivers.Chrome | WebDrivers.Edge | WebDrivers.Opera;
            var split = values.Split();
            Assert.AreEqual(3, split.Count());
            Assert.AreEqual(WebDrivers.Chrome, split.First());
            Assert.AreEqual(WebDrivers.Edge, split.ElementAt(1));
            Assert.AreEqual(WebDrivers.Opera, split.Last());
        }

        [TestMethod]
        public void DetectsDynamicAttribute()
        {
            Assert.IsFalse(TestEnum.Value1.IsDynamic());
            Assert.IsTrue(TestEnum.Value2.IsDynamic());
            Assert.IsTrue(TestEnum.Value3.IsDynamic());
        }

        private enum TestEnum
        {
            Value1,
            [Dynamic]
            Value2,
            [Dynamic]
            Value3
        }
    }
}
