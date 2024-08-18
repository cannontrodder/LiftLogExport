namespace LiftLog.Backup;

public interface IExport
{
    void ExportExercises(List<ExerciseRecord> exercises, StreamWriter writer);
}
