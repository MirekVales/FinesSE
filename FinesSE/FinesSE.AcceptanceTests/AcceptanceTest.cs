using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinesSE.Loader;

namespace FinesSE.AcceptanceTests
{
    [TestClass]
    public class AcceptanceTest
    {
        [TestMethod]
        public void Scenario()
        {
            using (var se = new SE())
            {
                se.Open("http://www.google.com");
                se.Type("identifier=lst-ib", "There is something I am looking for... ");
                se.Click("identifier=_fZl");
                se.Type("identifier=lst-ib", "Something...");
                se.Pause("3000");
            }
        }
    }
}
