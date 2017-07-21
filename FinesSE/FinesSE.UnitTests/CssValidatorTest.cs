using FinesSE.VisualRegression.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FinesSE.UnitTests
{
    [TestClass]
    public class CssValidatorTest
    {
        private readonly ICssValidator cssValidator = new CssValidator();

        [TestMethod]
        [DataRow("", true)]
        [DataRow("body{}", true)]
        [DataRow("body{ color: white; }", true)]
        [DataRow("#div1, .class1 { color: white; } ", true)]
        [DataRow("body{}}", false)]
        [DataRow("body{ body, #div1, .class1 { color: white; } }", false)]
        public void DetectsIncidents(string css, bool valid)
        {
             Assert.IsTrue(valid ? !cssValidator.Validate(css).Any() : cssValidator.Validate(css).Any());
        }
    }
}
