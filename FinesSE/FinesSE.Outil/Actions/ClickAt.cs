using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FinesSE.Outil.Actions
{
    public class ClickAt : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
            yield return typeof(Point);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.Cast<IEnumerable<IWebElement>>().First(), parameters.Cast<Point>().Last());

        public string Invoke(IEnumerable<IWebElement> elements, Point coordinates)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(DriverProvider.Get());
            foreach (var element in elements)
            {
                action
                    .MoveToElement(element, (int)coordinates.X, (int)coordinates.Y)
                    .Click()
                    .Perform();
            }
            return "";
        }
    }
}
