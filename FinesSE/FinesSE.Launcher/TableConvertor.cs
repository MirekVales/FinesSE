using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FinesSE.Launcher
{
    public class TableConvertor : ITableConvertor
    {
        public string ConvertToHtmlTables(string pipeSeparatedFormat)
            => string.Join(Environment.NewLine, GetTables(pipeSeparatedFormat));

        private IEnumerable<string> GetTables(string pipeSeparatedFormat)
        {
            foreach (var tableSegment in Regex.Split(pipeSeparatedFormat, @"\n\n"))
                yield return $"<table>{GetTable(tableSegment)}</table>";
        }

        private string GetTable(string tableSegment)
        {
            var rows = Regex.Split(tableSegment, @"\n")
                .Where(x => x.Length >= 2)
                .Select(x => $"<tr><td>{x.Substring(1, x.Length - 2).Replace("|","</td><td>")}</td></tr>").ToArray();

            return string.Join(Environment.NewLine, rows);
        }
    }
}
