using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FinesSE.Outil.Actions
{
    public class ClickAt : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
            yield return typeof(Point);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<LocatedElements>().First(), parameters.Cast<Point>().Last());

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
