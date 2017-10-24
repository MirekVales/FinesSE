using FinesSE.Bootstrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FinesSE.AcceptanceTests
{
    [TestClass]
    public class VisualRegressionTest
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
        public void SameImagesAreFoundSame()
        {
            se.TakeBaseScreen("id=img_1");
            se.SetScreenDiffTolerance("0");
            se.VerifyScreenDiff("id=img_1");
        }

        [TestCleanup]
        public void Cleanup()
        {
            se.Close();
            se.Dispose();
        }
    }
}
