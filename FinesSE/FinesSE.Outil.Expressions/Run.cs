using FinesSE.Contracts.Invokable;
using System.Diagnostics;

namespace FinesSE.Outil.Expressions
{
    public class Run : IStringAction
    {
        [EntryPoint]
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
