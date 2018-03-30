using System.Linq;

namespace FinesSE.Contracts.Exceptions
{
    public class XmlParseException : SlimException
    {
        public XmlParseException(string source, string exceptionMessage)
            : base($"Content starting with '{new string(source.ToCharArray().Take(15).ToArray())}...' " +
                  $"and ending with '...{new string(source.ToCharArray().Reverse().Take(15).ToArray())}' " +
                  $"is not a valid xml. {exceptionMessage}")
        {
        }
    }
}
