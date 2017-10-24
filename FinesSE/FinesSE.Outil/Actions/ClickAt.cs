using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Windows;

namespace FinesSE.Outil.Actions
{
    public class ClickAt : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements, Point coordinates)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(Context.Driver);
            elements
                .ConstraintCount(c => c > 0)
                .Elements
                .ForEach(e =>
                    action
                    .MoveToElement(e, (int)coordinates.X, (int)coordinates.Y)
                    .Click()
                    .Perform());
        }
    }
}
