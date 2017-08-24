using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;

public class OptionNotFoundException : SlimException
{
    public OptionNotFoundException(OptionLocator locator)
           : base($"An option that would satisfy the locator ({locator.Locator}) was not found")
    {
    }
}