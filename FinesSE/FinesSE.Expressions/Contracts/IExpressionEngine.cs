using System;

namespace FinesSE.Expressions.Contracts
{
    public interface IExpressionEngine
    {
        object Execute(string expression);

        T Execute<T>(string expression, Func<object, T> converter);
    }
}