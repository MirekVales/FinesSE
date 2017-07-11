using FinesSE.Contracts.Exceptions;

namespace FinesSE.VisualRegression.Contracts
{
    public class ComparisonAssertionException : SlimException
    {
        public ComparisonAssertionException(string elementId, string baseVersion, string referenceVersion, double difference, double tolerance)
            : base($"Difference {difference} % is bigger than tolerated ({tolerance} %) in compared versions ('{baseVersion}', '{referenceVersion}')")
        {
        }
    }
}
