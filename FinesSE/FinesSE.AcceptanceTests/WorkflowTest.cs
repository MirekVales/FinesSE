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

        [TestMethod]
        public void CanInterpolateVariables()
        {
            using (var se = new SE())
            {
                se.Do("var variable1 = 'false'");
                se.If("#{variable1}");
                Assert.AreEqual(se.Do("5*5"), "");
                se.Endif();
                Assert.AreEqual(se.Do("5*5"), "25");

                se.Do("var variable2 = 'true'");
                se.Do("var variable3 = 10");
                se.If("#{variable2}");
                Assert.AreEqual(se.Do("5*#{variable3}"), "50");
                se.Endif();
                Assert.AreEqual(se.Do("5*#{variable3}"), "50");
            }
        }
    }
}