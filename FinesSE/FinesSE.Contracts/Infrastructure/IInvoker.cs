using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IInvoker
    {
        IEnumerable<Type> ParameterTypes { get; }

        string Invoke(params object[] parameters);

        void SetAction(IAction action, IExecutionContext context);

        void AddInfo(string info);

        void AddImage(string pathToImage);
    }
}