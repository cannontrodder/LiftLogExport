namespace LiftLog.Backup;

public interface IExport
{
    void ExportExercises(ExportOptions options, List<ExerciseRecord> exercises, StreamWriter writer);
}
