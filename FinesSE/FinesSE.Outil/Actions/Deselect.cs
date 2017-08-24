using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FinesSE.Outil.Actions
{
    public class Deselect : IVoidAction
    {
        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
            yield return typeof(OptionLocator);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.First() as LocatedElements, parameters.Last() as OptionLocator);

        public IExecutionContext Context { get; set; }

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
