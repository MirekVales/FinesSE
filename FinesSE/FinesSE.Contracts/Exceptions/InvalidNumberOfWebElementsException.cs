namespace FinesSE.Contracts.Exceptions
{
    public class InvalidNumberOfWebElementsException : SlimException
    {
        public InvalidNumberOfWebElementsException(int actualCount)
               : base($"Invalid number of located web elements ({actualCount})")
        {
        }
    }
}
