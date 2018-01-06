using FinesSE.Launcher.Infrastructure;
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

            CreateRunner()
                .ExecuteAsync(new FileInfo(startOptions.InputFile), startOptions.TableFormat, startOptions.OutputFile)
                .Wait();
        }

        static FitRunner CreateRunner()
        {
            var assemblies = new[] {
                $"{nameof(FinesSE)}.{nameof(Bootstrapper)}.dll",
                $"{nameof(FinesSE)}.Contracts.dll",
                $"{nameof(FinesSE)}.Core.dll",
                $"{nameof(FinesSE)}.Drivers.dll",
                $"{nameof(FinesSE)}.Expressions.dll",
                $"{nameof(FinesSE)}.Outil.dll",
                $"{nameof(FinesSE)}.Outil.Expressions.dll",
                $"{nameof(FinesSE)}.Outil.Reports.dll",
                $"{nameof(FinesSE)}.Outil.VisualRegression.dll",
                $"{nameof(FinesSE)}.Reports.dll",
                $"{nameof(FinesSE)}.VisualRegression.dll",
            };
            var namespaces = new[] { $"{nameof(FinesSE)}.{nameof(Bootstrapper)}" };
            return new FitRunner(assemblies, namespaces);
        }
    }
}
