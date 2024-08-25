namespace LiftLog.Backup;
public record ExportOptions(
    bool Verbose,
    string BackupFilePath,
    string TemplateName
);
