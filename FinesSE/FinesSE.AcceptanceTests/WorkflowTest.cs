using FinesSE.Bootstrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinesSE.AcceptanceTests
{
    [TestClass]
    public class WorkflowTest
    {
        [TestMethod]
        public void IfCanDisableActions()
        {
            using (var se = new SE())
            {
                Assert.AreEqual(se.Do("5*5"), "25");
                se.If("false");
                Assert.AreEqual(se.Do("5*5"), "");
            }
        }

        [TestMethod]
        public void IfBlockCanBeEnded()
        {
            using (var se = new SE())
            {
                Assert.AreEqual(se.Do("5*5"), "25");
                se.If("false");
                Assert.AreEqual(se.Do("5*5"), "");
                se.Endif();
                Assert.AreEqual(se.Do("5*5"), "25");
            }
        }

        [TestMethod]
        public void SupportsMultipleIfBlocks()
        {
            using (var se = new SE())
            {
                Assert.AreEqual(se.Do("5*5"), "25");
                se.If("true");
                Assert.AreEqual(se.Do("5*5"), "25");
                se.If("false");
                Assert.AreEqual(se.Do("5*5"), "");
                se.Endif();
                se.If("true");
                Assert.AreEqual(se.Do("5*5"), "25");
                se.Endif();
                Assert.AreEqual(se.Do("5*5"), "25");
            }
        }
    }
}