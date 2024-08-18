namespace LiftLog.Backup;
public record ExerciseRecord(
    DateOnly Date,
    TimeOnly Time,
    string SessionName,
    string ExerciseName,
    int TargetReps,
    int ActualReps,
    decimal Weight,
    decimal BodyWeight,
    string Notes);
