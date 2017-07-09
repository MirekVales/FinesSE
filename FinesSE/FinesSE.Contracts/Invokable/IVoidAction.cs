using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Invokable
{
    public interface IVoidAction
    {
        IEnumerable<Type> GetParameterTypes();

        void Invoke(params object[] parameters);
    }
}
