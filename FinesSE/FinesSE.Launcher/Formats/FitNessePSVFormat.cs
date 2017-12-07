using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FinesSE.Launcher.Formats
{
    public class FitNessePSVFormat : IFormatParser
    {
        public TableFormat Format => TableFormat.FitNessePSV;

        public string Parse(string input)
            => string.Join(Environment.NewLine, GetTables(input));

        IEnumerable<string> GetTables(string pipeSeparatedFormat)
        {
            foreach (var tableSegment in Regex.Split(pipeSeparatedFormat, @"\n\n"))
                yield return $"<table>{GetTable(tableSegment)}</table>";
        }

        string GetTable(string tableSegment)
            => string.Join(
                Environment.NewLine,
                Regex.Split(tableSegment, @"\n")
                    .Select(r => r.Trim())
                    .Select(GetRow)
                );

        string GetRow(string row)
        {
            var builder = new StringBuilder("<tr>");

            const string RowPattern = @"((?<=\|)((\!\-)(.*?)(\-\!))|([^|]+?)(?=\|))|((?<=\|)(?=\|))";
            foreach (Match match in Regex.Matches(row, RowPattern))
                builder.Append($"<td>{match.Value}</td>");

            builder.Append("</tr>");
            builder.Replace("!-", "");
            builder.Replace("-!", "");
            RemoveCommentedOutLines(ref builder);
            return builder.ToString();
        }

        void RemoveCommentedOutLines(ref StringBuilder builder)
        {
            const string CommentedOutLinePattern = @"(?m)^\#.*\n";
            foreach (Match match in Regex.Matches(builder.ToString(), CommentedOutLinePattern))
                builder.Replace(match.Value, "");
        }
    }
}
