using FinesSE.Launcher.Infrastructure;
using System;
using System.Collections.Generic;

namespace FinesSE.Launcher.Formats
{
    public class FormatConvertor
    {
        private readonly Dictionary<TableFormat, Func<IFormatParser>> convertors;

        public FormatConvertor()
        {
            convertors = new Dictionary<TableFormat, Func<IFormatParser>>()
            {
                { TableFormat.FitNessePSV, () => new FitNessePSVFormat() },
                { TableFormat.FitNesseTable, () => new FitNesseTableFormat() },
                { TableFormat.SeleneseTable, () => new SeleneseTableFormat() }
            };
        }

        public string Convert(string input, TableFormat format)
        {
            if (!convertors.ContainsKey(format))
                throw new UnsupportedFormatException(format);

            return convertors[format]().Parse(input);
        }
    }
}
