using FinesSE.Bootstrapper;
using FinesSE.Contracts.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FinesSE.AcceptanceTests
{
    [TestClass]
    public class WebDriverTest
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
        }

        [TestMethod]
        [DataRow("css=.css_button_1", "Lorem ipsum", "1", true)]
        [DataRow("css=.css_button_1", "Lorem ipsum", "10000", false)]
        public void PageLoadCanTimeout(
            string locator,
            string expectedText,
            string timeout,
            bool fires)
        {
            se.SetPageLoadTimeout(timeout);
            if (fires)
                Assert.ThrowsException<ActionException>(
                    () =>
                    {
                        se.Open(TestSiteUrl);
                        se.VerifyText(locator, expectedText);
                    });
            else
            {
                se.Open(TestSiteUrl);
                se.VerifyText(locator, expectedText);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            se.Close();
            se.Dispose();
        }
    }
}