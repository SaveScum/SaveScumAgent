using CommandLine;
using CommandLine.Text;
using SevenZip;

namespace AutoBackup
{
    internal class CommandLineOptions
    {
        [Option('w', DefaultValue = null, HelpText = "Directory to watch. Defaults to current directory.")]
        public string WatchDirectory { get; set; }

        [Option('d', DefaultValue = 10,
            HelpText = "After a change is detected, wait this many seconds before triggering the archive script")]
        public int Delay { get; set; }

        [Option('b', Required = true,
            HelpText =
                "Folder to save the archived copies in. DO NOT make this a subfolder of the watched folder, or else you'll just cause an endless loop."
            )]
        public string BackupsDirectory { get; set; }

        [Option('k', DefaultValue = 5, HelpText = "Number of old backups to keep")]
        public int ToKeep { get; set; }

        [Option('a', DefaultValue = OutArchiveFormat.SevenZip, HelpText = "Archive format.")]
        public OutArchiveFormat Format { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this);
        }
    }
}
