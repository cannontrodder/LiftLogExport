using CommandLine;
using LiftLog.Backup;

namespace LiftLogCLI;

internal static class Program
{
    static async Task<int> Main(string[] args)
    {
        ParserResult<Options> res = Parser.Default.ParseArguments<Options>(args);

        if (res.Errors.Any())
        {
            Console.Error.WriteLine("Invalid options.");
            return 1;
        }

        ExportOptions options = res.Value.ToExportOptions();

        FileInfo file = new FileInfo(options.BackupFilePath);
        if (!file.Exists)
        {
            Console.Error.WriteLine($"File not found: {options.BackupFilePath}.");
            return 1;
        }

        switch (res.Value.OutputFormat.ToLowerInvariant())
        {
            case "csv":
                await ExportToCsv(options, file);
                break;
            case "template":
                await ExportToTemplate(options, file);
                break;
            default:
                Console.Error.WriteLine($"Invalid output format: {res.Value.OutputFormat}.");
                return 1;
        }

        return 0;
    }

    static async Task ExportToCsv(ExportOptions options, FileInfo file)
    {
        List<ExerciseRecord> data = await BackupReader.LoadExerciseRecords(file.FullName);

        CsvExport exporter = new();
        exporter.ExportExercises(options, data, new StreamWriter(Console.OpenStandardOutput()));
    }

    static async Task ExportToTemplate(ExportOptions options, FileInfo file)
    {
        List<ExerciseRecord> data = await BackupReader.LoadExerciseRecords(file.FullName);

        TemplateExport exporter = new();
        exporter.ExportExercises(options, data, new StreamWriter(Console.OpenStandardOutput()));
    }
}
