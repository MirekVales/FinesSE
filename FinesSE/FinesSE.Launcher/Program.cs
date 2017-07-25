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

            var assemblies = new[] { $"{nameof(Loader)}.dll" };
            var namespaces = new[] { nameof(Loader) };
            SlimRunner
                .ExecuteAsync(assemblies, namespaces, new FileInfo(startOptions.InputFile), startOptions.OutputFile)
                .Wait();
        }
    }
}
