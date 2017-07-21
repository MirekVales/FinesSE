using ExCSS;
using System.Collections.Generic;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class CssValidator : ICssValidator
    {
        public IEnumerable<CssValidationIncident> Validate(string css)
        {
            var sheet = new Parser().Parse(css);

            foreach (var error in sheet.Errors)
                yield return new CssValidationIncident(error.Column, error.Line, error.Message);
        }
    }
}
