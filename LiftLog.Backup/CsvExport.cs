using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace LiftLog.Backup;

public class CsvExport : IExport
{
    public class ExerciseMap : ClassMap<ExerciseRecord>
    {
        public ExerciseMap()
        {
            int idx = 0;

            Map(e => e.Date)
                .Index(idx++)
                .Convert(e => e.Value.Date.ToDateTime(e.Value.Time).ToString("o"))
                .Name("Time");

            Map(e => e.SessionName).Index(idx++).Name("Session");
            Map(e => e.BodyWeight).Index(idx++).Name("BW");
            Map(e => e.ExerciseName).Index(idx++).Name("Exercise");
            Map(e => e.Weight).Index(idx++).Name("Weight");
            Map(e => e.TargetReps).Index(idx++).Name("Target");
            Map(e => e.ActualReps).Index(idx++).Name("Actual");
            Map(e => e.Notes).Index(idx++).Name("Notes");
        }
    }

    public void ExportExercises(List<ExerciseRecord> exercises, StreamWriter writer)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
        };

        using (var csv = new CsvWriter(writer, config))
        {
            csv.Context.RegisterClassMap<ExerciseMap>();
            // csv.WriteHeader<ExerciseRecord>();
            csv.WriteRecords(exercises);
        }
    }
}
