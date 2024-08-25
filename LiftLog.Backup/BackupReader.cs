using LiftLog.Ui.Models.ExportedDataDao;

namespace LiftLog.Backup;

public static class BackupReader
{
    public static async Task<byte[]> LoadAndDecompressFile(string path)
    {
        using System.IO.Compression.GZipStream gzip = new(
            File.OpenRead(path),
            System.IO.Compression.CompressionMode.Decompress);

        using MemoryStream memoryStream = new();
        await gzip.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public static async Task<ExportedDataDaoV2> LoadExport(string path)
    {
        byte[] data = await LoadAndDecompressFile(path);

        return ExportedDataDaoV2.Parser.ParseFrom(data);
    }

    public static async Task<List<ExerciseRecord>> LoadExerciseRecords(string path)
    {
        ExportedDataDaoV2 data = await LoadExport(path);

        return data.Sessions.SelectMany(session =>
            session.RecordedExercises.SelectMany(re =>
                re.PotentialSets
                    .Where(s => s.RecordedSet != null)
                    .Select(set =>
                        new ExerciseRecord(
                            session.Date,
                            set.RecordedSet.CompletionTime,
                            session.SessionName,
                            re.ExerciseBlueprint.Name,
                            re.ExerciseBlueprint.RepsPerSet,
                            set.RecordedSet.RepsCompleted,
                            set.Weight,
                            session.Bodyweight,
                            re.Notes ?? string.Empty
                            ))))
            .OrderBy(r => r.Date)
            .ThenBy(r => r.Time)
            .ToList();
    }
}
