using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FinesSE.Outil.Expressions
{
    public class Run : IAction
    {
        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(string);
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as string, parameters.Last() as string);

        public string Invoke(string filePath, string arguments)
        {
            var startInfo = new ProcessStartInfo()
            {
                Arguments = arguments,
                CreateNoWindow = true,
                FileName = filePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var process = Process.Start(startInfo))
            {
                var output = process.StandardOutput.ReadToEnd();
                var errorOutput = process.StandardError.ReadToEnd();

                process.WaitForExit();

                return string.IsNullOrWhiteSpace(errorOutput)
                    ? output
                    : errorOutput;
            }
        }
    }
}
