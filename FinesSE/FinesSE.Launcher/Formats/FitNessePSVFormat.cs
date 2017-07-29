using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FinesSE.Launcher.Formats
{
    public class FitNessePSVFormat : IFormatParser
    {
        public TableFormat Format => TableFormat.FitNessePSV;

        public string Parse(string input)
            => string.Join(Environment.NewLine, GetTables(input));

        private IEnumerable<string> GetTables(string pipeSeparatedFormat)
        {
            foreach (var tableSegment in Regex.Split(pipeSeparatedFormat, @"\n\n"))
                yield return $"<table>{GetTable(tableSegment)}</table>";
        }

        private string GetTable(string tableSegment)
            => string.Join(
                Environment.NewLine, 
                Regex.Split(tableSegment, @"\n")
                    .Select(r => r.Trim())
                    .Select(GetRow)
                );

        private string GetRow(string row)
            => $"<tr><td>{string.Join("</td><td>", row.Trim('|').Split('|'))}</td></tr>";
    }
}
