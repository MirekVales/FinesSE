using System;
using FinesSE.Expressions.Contracts;
using Jint;

namespace FinesSE.Expressions
{
    public class ExpressionEngine : IExpressionEngine
    {
        readonly static Engine engine;

        static ExpressionEngine()
        {
            engine = new Engine();
        }

        public object Execute(string expression)
            => engine
            .Execute(expression)
            .GetCompletionValue();

        public T Execute<T>(string expression, Func<object, T> converter)
            => converter(engine
            .Execute(expression)
            .GetCompletionValue() + "");
    }
}
