using System;
using System.Text.RegularExpressions;
using FinesSE.Contracts.Invokable;
using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Outil.Actions
{
    public class SetPageLoadTimeout : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string pageLoadTimeoutLength)
        {
            var length = int.Parse(Regex.Match(pageLoadTimeoutLength, "\\d{1,6}").Value);
            Context.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(length);
        }
    }
}
