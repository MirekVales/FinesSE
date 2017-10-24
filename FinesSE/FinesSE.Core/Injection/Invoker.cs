using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FinesSE.Core.Injection
{
    public class Invoker
    {
        readonly IAction action;
        readonly Type actionType;
        readonly MethodInfo entryPointMethod;
        readonly Type[] entryPointParameters;

        public IEnumerable<Type> ParameterTypes
            => entryPointParameters;

        public Invoker(IAction action)
        {
            this.action = action;
            actionType = action.GetType();

            entryPointMethod = actionType
                .GetMethods()
                .Where(m => m.GetCustomAttribute<EntryPointAttribute>() != null)
                .SingleOrDefault();

            if (entryPointMethod == null)
                throw new MethodNotFoundException(actionType.Name);

            entryPointParameters = entryPointMethod
                .GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();
        }

        public string Invoke(params object[] parameters)
        {
            var parameterTypes = parameters
                .Select(p => p.GetType())
                .ToArray();

            if (TypesMatchable(entryPointParameters, parameterTypes))
                return entryPointMethod.Invoke(action, parameters) + "";

            throw new MethodNotFoundException(actionType.Name, parameterTypes);
        }

        bool TypesMatchable(Type[] entryPointTypes, Type[] passedTypes)
        {
            if (entryPointTypes.Length != passedTypes.Length)
                return false;

            var passedParameterTypes = new Queue<Type>(passedTypes);
            foreach (var entryPointParameterType in entryPointTypes)
            {
                var passedParameter = passedParameterTypes.Dequeue();
                if (!entryPointParameterType.IsAssignableFrom(passedParameter))
                    return false;
            }
            return true;
        }
    }
}
