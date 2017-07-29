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

        static SlimRunner CreateRunner()
        {
            var assemblies = new[] { $"{nameof(FinesSE)}.{nameof(Loader)}.dll" };
            var namespaces = new[] { $"{nameof(FinesSE)}.{nameof(Loader)}" };
            return new SlimRunner(assemblies, namespaces);
        }
    }
}
