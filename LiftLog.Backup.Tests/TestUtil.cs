namespace LiftLog.Backup.Tests;
internal static class TestUtil
{
    public static string GetTestFilePath(string filename)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", filename);
    }
}
