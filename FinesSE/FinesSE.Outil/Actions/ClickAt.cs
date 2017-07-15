using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FinesSE.Outil.Actions
{
    public class ClickAt : IVoidAction
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
            yield return typeof(Point);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<LocatedElements>().First(), parameters.Cast<Point>().Last());

        public void Invoke(LocatedElements elements, Point coordinates)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(DriverProvider.Get());
            elements
                .ConstraintCount(c => c > 0)
                .Elements
                .ToList()
                .ForEach(e => 
                    action
                    .MoveToElement(e, (int)coordinates.X, (int)coordinates.Y)
                    .Click()
                    .Perform());
        }
    }
}
