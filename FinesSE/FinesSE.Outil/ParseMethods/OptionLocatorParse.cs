using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Text.RegularExpressions;

public class OptionLocatorParse : IParseMethod
{
    public Type ParsedType
        => typeof(OptionLocator);

    public object Invoke(string input)
    {
        var match = Regex.Match(
            input,
            "(index|value|label)=(.+)",
            RegexOptions.IgnoreCase);

        if (!match.Success)
            throw new ArgumentException();

        return new OptionLocator()
        {
            Type = match.Groups[1].Value,
            Value = match.Groups[2].Value,
        };
    }
}
