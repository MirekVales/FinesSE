using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class DoubleClick : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<LocatedElements>().First());

        public void Invoke(LocatedElements elements)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(Context.Driver);
            elements
              .ConstraintCount(c => c > 0)
              .Elements
              .ToList()
              .ForEach(x =>
                action
                .MoveToElement(x)
                .DoubleClick()
                .Perform());
        }
    }
}
