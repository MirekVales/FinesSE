using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinesSE.Bootstrapper;
using System;
using System.IO;

namespace FinesSE.AcceptanceTests
{
    [TestClass]
    public class LocatorTest
    {
        private SE se;

        public string TestSiteUrl =>
            @"file://" + Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"TestSites\1\index.html");

        [TestInitialize]
        public void Initialize()
        {
            se = new SE();
            se.Open(TestSiteUrl);
        }

        [TestMethod]
        [DataRow("css=.css_button_1", "Lorem ipsum")]
        [DataRow("id=button_1", "Lorem ipsum")]
        [DataRow("link=Lorem ipsum", "Lorem ipsum")]
        [DataRow("name=name_button_2", "Dolor sit amet")]
        [DataRow("tagname=h4", "End")]
        [DataRow("xpath=html/body/h1[1]", "Lorem ipsum dolor sit amet in")]
        public void LocatorsGrabCorrectElement(string locator, string expectedText)
            => se.VerifyText(locator, expectedText);

        [TestCleanup]
        public void Cleanup()
        {
            se.Close();
            se.Dispose();
        }
    }
}