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

    [Option(
        'o',
        "output",
        Required = false,
        HelpText = "Output format. 'CSV' or 'Template'.",
        Default = "CSV")]
    public string OutputFormat { get; set; } = string.Empty;

    [Option(
        't',
        "template",
        Required = false,
        HelpText = "Template name. 'OrgMode'",
        Default = "OrgMode")]
    public string TemplateName { get; set; } = string.Empty;

    public ExportOptions ToExportOptions()
    {
        return new ExportOptions(Verbose, File, TemplateName);
    }
}
