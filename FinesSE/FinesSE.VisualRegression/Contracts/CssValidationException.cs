using FinesSE.Contracts.Exceptions;
using FinesSE.VisualRegression.Infrastructure;
using System.Collections.Generic;
using System;
using System.Linq;

namespace FinesSE.VisualRegression.Contracts
{
    public class CssValidationException : SlimException
    {
        public CssValidationException(IEnumerable<CssValidationIncident> incidents)
            : base($"Css validation failed due to following incidents: {Environment.NewLine}{Format(incidents)}")
        {
        }

        private static object Format(IEnumerable<CssValidationIncident> incidents)
            => string.Join(Environment.NewLine, incidents.Select(x => x.ToString()));
    }
}
