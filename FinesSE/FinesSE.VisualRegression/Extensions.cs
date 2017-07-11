using System;

namespace FinesSE.VisualRegression
{
    public static class Extensions
    {
        public static string FallbackEmptyString(this string value, Func<string> fallback)
            => string.IsNullOrWhiteSpace(value) ? fallback() : value;
    }
}
