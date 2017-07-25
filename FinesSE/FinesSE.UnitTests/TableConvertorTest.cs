using FinesSE.Launcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinesSE.UnitTests
{
    [TestClass]
    public class TableConvertorTest
    {
        readonly ITableConvertor tableConvertor = new TableConvertor();

        [TestMethod]
        [DataRow("|", "<table></table>")]
        [DataRow("||", "<table><tr><td></td></tr></table>")]
        [DataRow("|1|","<table><tr><td>1</td></tr></table>")]
        [DataRow("|1|2|", "<table><tr><td>1</td><td>2</td></tr></table>")]
        [DataRow("|1 |2 |", "<table><tr><td>1 </td><td>2 </td></tr></table>")]
        public void ParsesRows(string input, string expectedOutput)
        {
            Assert.AreEqual(expectedOutput, tableConvertor.ConvertToHtmlTables(input));
        }

        [TestMethod]
        [DataRow(
            "|1|2|\n|1|2|3|\n|1|", 
            "<table><tr><td>1</td><td>2</td></tr>\r\n<tr><td>1</td><td>2</td><td>3</td></tr>\r\n<tr><td>1</td></tr></table>")]
        public void ParsesMultilineTable(string input, string expectedOutput)
        {
            Assert.AreEqual(expectedOutput, tableConvertor.ConvertToHtmlTables(input));
        }
    }
}
