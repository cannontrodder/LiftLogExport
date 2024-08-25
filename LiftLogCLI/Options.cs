using CommandLine;

namespace LiftLog.Backup;
internal class Options
{
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Verbose { get; set; }

    [Option(
        'f',
        "file",
        Required = true,
        HelpText = "Path to backup file to read.",
        Default = "export.liftlogbackup.gz")]
    public string File { get; set; } = string.Empty;

    public ExportOptions ToExportOptions()
    {
        return new ExportOptions(Verbose, File);
    }
}
