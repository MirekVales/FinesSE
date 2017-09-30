using FinesSE.Contracts.Invokable;
using FinesSE.Expressions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Expressions
{
    public class Do : IAction
    {
        public IExpressionEngine Engine { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Engine.Execute(parameters.First() + "") + "";
    }
}
