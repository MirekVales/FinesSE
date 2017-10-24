using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System;

namespace FinesSE.Outil.Actions
{
    public class Deselect : IVoidAction
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
                { "index", (e, v) => e.DeselectByIndex(int.Parse(v)) },
                { "label", (e, v) => e.DeselectByText(v) },
                { "value", (e, v) => e.DeselectByValue(v) }
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
