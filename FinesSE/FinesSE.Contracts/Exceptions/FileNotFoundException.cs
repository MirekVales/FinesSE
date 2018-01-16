namespace FinesSE.Contracts.Exceptions
{
    public class FileNotFoundException : SlimException
    {
        public FileNotFoundException(string message) : base(message)
        {
        }
    }
}
