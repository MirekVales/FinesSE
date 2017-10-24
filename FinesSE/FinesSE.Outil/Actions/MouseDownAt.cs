using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Windows;

namespace FinesSE.Outil.Actions
{
    public class MouseDownAt : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements, Point coordinate)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(Context.Driver);
            elements
              .ConstraintCount(c => c == 1)
              .Elements
              .ForEach(x =>
                action
                .MoveToElement(x, (int)coordinate.X, (int)coordinate.Y)
                .ClickAndHold()
                .Perform());
        }
    }
}
