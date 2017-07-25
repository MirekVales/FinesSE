using CommandLine;

namespace FinesSE.Launcher
{
    public class StartOptions
    {
        [Option('f', "file", Required = true, HelpText = "A path to a single test file")]
        public string InputFile { get; set; }

        [Option('e', "outputfile", Required = true, HelpText = "A path to an output file to be created")]
        public string OutputFile { get; set; }
    }
}
