using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace FinesSE.Outil.Actions
{
    public class Select : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements, OptionLocator optionLocator)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(Context.Driver);
            elements
              .ConstraintCount(c => c == 1)
              .Elements
              .ForEach(x => PerformSelection(new SelectElement(x), optionLocator));
        }

        private void PerformSelection(SelectElement selectElement, OptionLocator optionLocator)
        {
            var locators = new Dictionary<string, Action<SelectElement, string>>()
            {
                { "index", (e, v) => e.SelectByIndex(int.Parse(v)) },
                { "label", (e, v) => e.SelectByText(v) },
                { "value", (e, v) => e.SelectByValue(v) }
            };

            foreach (var locator in locators)
                if (optionLocator.Type.Equals(locator.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    locator.Value(selectElement, optionLocator.Value);
                    return;
                }

            throw new OptionNotFoundException(optionLocator);
        }
    }
}
