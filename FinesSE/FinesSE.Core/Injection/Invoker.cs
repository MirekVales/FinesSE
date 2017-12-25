using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FinesSE.Core.Injection
{
    public class Invoker : IInvoker
    {
        IAction action;
        IExecutionContext context;
        Type actionType;
        MethodInfo entryPointMethod;
        Type[] entryPointParameters;
        Guid currentTestId;

        public IReportBuilder Builder { get; set; }

        public IEnumerable<Type> ParameterTypes
            => entryPointParameters;

        public Invoker()
        { }

        public void SetAction(IAction action, IExecutionContext context)
        {
            this.action = action;
            this.context = context;
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
            var testScopeStarted = StartInvocationScope(out Guid id);
            currentTestId = id;

            var parameterTypes = parameters
                .Select(p => p?.GetType())
                .ToArray();

            if (TypesMatchable(entryPointParameters, parameterTypes))
            {
                string result;
                try
                {
                    result = entryPointMethod.Invoke(action, parameters) + "";
                    if (testScopeStarted)
                        SetTestInfo();
                }
                catch (SlimException e)
                {
                    if (testScopeStarted)
                    {
                        SetTestInfo();
                        Builder.EndTest(id, LogStatus.Error, e);
                    }

                    throw e;
                }

                if (testScopeStarted)
                    Builder.EndTest(id, LogStatus.Pass);

                return result;
            }

            var errorException = new MethodNotFoundException(actionType.Name, parameterTypes);
            if (testScopeStarted)
                Builder.EndTest(id, LogStatus.Error, errorException);
            throw errorException;
        }

        public void AddInfo(string info)
            => Builder.LogTest(currentTestId, LogStatus.Info, info);

        public void AddImage(string pathToImage)
            => Builder.AppendScreenshot(currentTestId, pathToImage);

        bool StartInvocationScope(out Guid id)
        {
            if (typeof(IReportable).IsAssignableFrom(action.GetType()))
            {
                id = Guid.NewGuid();
                var reportable = action as IReportable;
                Builder.StartTest(id, reportable.Name, reportable.Description);
                return true;
            }
            id = Guid.Empty;
            return false;
        }

        void SetTestInfo()
        {
            if (typeof(IReportable).IsAssignableFrom(action.GetType()))
            {
                var reportable = action as IReportable;
                var tags = Builder.GetTags(reportable, context).ToArray();
                Builder.SetTestInfo(currentTestId, reportable.Name, reportable.Description, tags);
            }
        }

        bool TypesMatchable(Type[] entryPointTypes, Type[] passedTypes)
        {
            if (entryPointTypes.Length != passedTypes.Length)
                return false;

            var passedParameterTypes = new Queue<Type>(passedTypes);
            foreach (var entryPointParameterType in entryPointTypes)
            {
                var passedParameter = passedParameterTypes.Dequeue();

                if (passedParameter != null
                    && !entryPointParameterType.IsAssignableFrom(passedParameter))
                    return false;
            }
            return true;
        }
    }
}
