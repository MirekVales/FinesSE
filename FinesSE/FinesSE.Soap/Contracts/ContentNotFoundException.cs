using FinesSE.Contracts.Exceptions;

namespace FinesSE.Soap.Contracts
{
    public class ContentNotFoundException : SlimException
    {
        public ContentNotFoundException(string id) : base($"A stored content with id {id} was not found")
        {
        }
    }
}
