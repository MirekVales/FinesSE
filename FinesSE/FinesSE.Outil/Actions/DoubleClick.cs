using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class DoubleClick : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(Context.Driver);
            elements
              .ConstraintCount(c => c > 0)
              .Elements
              .ForEach(x =>
                action
                .MoveToElement(x)
                .DoubleClick()
                .Perform());
        }
    }
}
