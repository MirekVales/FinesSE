using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Invokable
{
    public interface IAction
    {
        IEnumerable<Type> GetParameterTypes();

        string Invoke(params object[] parameters);
    }
}
