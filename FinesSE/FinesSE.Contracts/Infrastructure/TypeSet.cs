using System;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public class TypeSet
    {
        readonly Dictionary<Type, object> store = new Dictionary<Type, object>();

        public bool TryGet<T>(out T item)
        {
            if (store.ContainsKey(typeof(T)))
            {
                item = (T)store[typeof(T)];
                return true;
            }

            item = default(T);
            return false;
        }

        public bool Set<T>(T item)
        {
            var exists = store.ContainsKey(typeof(T));
            store[typeof(T)] = item;
            return exists;
        }
    }
}
