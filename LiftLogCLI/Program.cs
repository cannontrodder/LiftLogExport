using System.CommandLine;
using LiftLog.Backup;

namespace LiftLogCLI
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            Option<FileInfo?> fileOption = new(
                name: "--file",
                getDefaultValue: () => new FileInfo("export.liftlogbackup.gz"),
                description: "The backup file to read.");

            RootCommand rootCommand = new("Export to CSV");
            rootCommand.AddOption(fileOption);

            rootCommand.SetHandler(async (file) =>
            {
                await ExportToCsv(file!);
            },
                fileOption);

            return await rootCommand.InvokeAsync(args);
        }

        static async Task ExportToCsv(FileInfo file)
        {
            List<ExerciseRecord> data = await BackupReader.LoadExerciseRecords(file.FullName);

            CsvExport exporter = new();
            exporter.ExportExercises(data, new StreamWriter(Console.OpenStandardOutput()));
        }
    }
}
