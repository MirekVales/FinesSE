using System.Collections.Generic;

namespace FinesSE.VisualRegression.Infrastructure
{
    public interface ICssValidator
    {
        IEnumerable<CssValidationIncident> Validate(string css);
    }
}