using OpenQA.Selenium;
using System.Collections.Generic;

namespace FinesSE.Contracts.Invokable
{
    public interface ILocator
    {
        string Regex { get; }

        IEnumerable<IWebElement> Locate(string value);
    }
}
