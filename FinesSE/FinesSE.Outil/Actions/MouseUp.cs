using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class MouseUp : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(Context.Driver);
            elements
              .ConstraintCount(c => c == 1)
              .Elements
              .ForEach(x =>
                action
                .Release(x)
                .Perform());
        }
    }
}
