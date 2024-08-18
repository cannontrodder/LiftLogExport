using LiftLog.Ui.Models.ExportedDataDao;

namespace LiftLog.Backup.Tests;

public class BackupTests
{
    [Fact]
    public async Task LoadAndDecompress()
    {
        string path = TestUtil.GetTestFilePath("testexport1.gz");
        byte[] data = await BackupReader.LoadAndDecompressFile(path);
        Assert.True(data.Length > 100);
    }

    [Fact]
    public async Task Deserialize()
    {
        string path = TestUtil.GetTestFilePath("testexport1.gz");
        ExportedDataDaoV2 data = await BackupReader.LoadExport(path);
        Assert.Equal(5, data.Sessions.Count);
    }

    [Fact]
    public async Task LoadRecords()
    {
        string path = TestUtil.GetTestFilePath("testexport1.gz");
        List<ExerciseRecord> data = await BackupReader.LoadExerciseRecords(path);
        ;
    }
}
