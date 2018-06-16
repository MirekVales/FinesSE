using CommandLine;
using FinesSE.Launcher.Formats;

namespace FinesSE.Launcher
{
    public class StartOptions
    {
        [Option('f', "file", Required = false, HelpText = "A path to a single test file")]
        public string InputFile { get; set; }

        [Option('t', "format", Required = false, HelpText = "A format of input data")]
        public TableFormat TableFormat { get; set; }

        [Option('e', "outputfile", Required = false, HelpText = "A path to an output file to be created")]
        public string OutputFile { get; set; }

        [Option('r', "terminateBrowserProcesses", Required = false, HelpText = "Indicates whether the active processes should be terminated")]
        public bool TerminateBrowserProcesses { get; set; }
    }
}
