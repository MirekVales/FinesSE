using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using FinesSE.Expressions.Contracts;
using LightInject.Interception;
using log4net;
using System.Linq;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.Expressions
{
    public class InterpolationInterceptor : ICustomInterceptor
    {
        public ILog Log { get; set; }
        public IKernel Kernel { get; set; }
        public IExpressionEngine ExpressionEngine { get; set; }

        const string ExpressionPattern = @"(#\{)(.*?)(\})";

        public object Invoke(IInvocationInfo invocationInfo)
        {
            var typeName = invocationInfo.Method.GetGenericArgumentsName().First();
            var userFriendlyName = typeName.Split('.').Last();
            if (Kernel.CanGet<IAction>(typeName) || Kernel.CanGet<IWorkflowAction>(typeName))
            {
                Interpolate(ref invocationInfo);
            }

            return invocationInfo.Proceed();
        }

        void Interpolate(ref IInvocationInfo invocationInfo)
        {
            var index = 0;
            foreach (var argument in invocationInfo.Arguments)
            {
                if (argument is string argumentValue)
                {
                    invocationInfo.Arguments[index] = GetInterpolatedValue(argumentValue);
                }
                else if (argument is string[] subArgumentValues)
                {
                    var subIndex = 0;
                    foreach (var subArgument in subArgumentValues)
                    {
                        (invocationInfo.Arguments[index] as string[])[subIndex]
                            = GetInterpolatedValue(subArgument);
                        subIndex++;
                    }
                }
                else
                    Log.Debug($"Uninterpolable argument type {argument.GetType().Name}");
                index++;
            }
        }
        
        string GetInterpolatedValue(string argumentValue)
        {
            if (string.IsNullOrEmpty(argumentValue))
                return argumentValue;

            var interpolatedValue = argumentValue;
            foreach (Match match in Regex.Matches(argumentValue, ExpressionPattern))
            {
                var value = ExpressionEngine.Execute(match.Groups[2].Value) + "";
                interpolatedValue = interpolatedValue.Replace(match.Value, value);
                Log.Debug($"Expression {match.Value} interpolated with {value}");
            }

            return interpolatedValue;
        }
    }
}
