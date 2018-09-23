using FinesSE.Core.Environment;
using FinesSE.Launcher.Infrastructure;
using System;
using System.IO;

namespace FinesSE.Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var startOptions = new StartOptions();

            if (!CommandLine.Parser.Default.ParseArgumentsStrict(args, startOptions))
                throw new InvalidArgumentsException("Provided arguments are not valid");

            if (startOptions.TerminateBrowserProcesses)
            {
                var count = new ProcessListStorage().CleanList();
                Console.WriteLine($"{count} processes closed");
            }

            if (string.IsNullOrWhiteSpace(startOptions.InputFile)
                || string.IsNullOrWhiteSpace(startOptions.OutputFile))
                return;

            CreateRunner()
                .ExecuteAsync(new FileInfo(startOptions.InputFile), startOptions.TableFormat, startOptions.OutputFile)
                .Wait();
        }

        static FitRunner CreateRunner()
        {
            var assemblies = new[] {
                $"{nameof(FinesSE)}.{nameof(Bootstrapper)}.dll"
            };
            var namespaces = new[] { $"{nameof(FinesSE)}.{nameof(Bootstrapper)}" };
            return new FitRunner(assemblies, namespaces);
        }
    }
}
