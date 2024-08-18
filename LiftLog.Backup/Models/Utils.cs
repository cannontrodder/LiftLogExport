namespace LiftLog.Ui.Models;
public partial class DateOnlyDao
{
    public DateOnlyDao(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    public static implicit operator DateOnly(DateOnlyDao dao) => new(dao.Year, dao.Month, dao.Day);

    public static implicit operator DateOnlyDao(DateOnly model) =>
        new(model.Year, model.Month, model.Day);

    public static implicit operator DateOnly?(DateOnlyDao? dao) =>
        dao is null ? null : (DateOnly)dao;

    public static implicit operator DateOnlyDao?(DateOnly? value) =>
        value is null ? null : (DateOnlyDao)value.Value;
}

public partial class TimeOnlyDao
{
    public TimeOnlyDao(int hour, int minute, int second, int millisecond)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    public static implicit operator TimeOnly(TimeOnlyDao dao) =>
        new(dao.Hour, dao.Minute, dao.Second, dao.Millisecond);

    public static implicit operator TimeOnlyDao(TimeOnly model) =>
        new(model.Hour, model.Minute, model.Second, model.Millisecond);

    public static implicit operator TimeOnly?(TimeOnlyDao? dao) =>
        dao is null ? null : (TimeOnly)dao;

    public static implicit operator TimeOnlyDao?(TimeOnly? value) =>
        value is null ? null : (TimeOnlyDao)value.Value;
}

public partial class DecimalValue
{
    private const decimal NanoFactor = 1_000_000_000;

    public DecimalValue(long units, int nanos)
    {
        Units = units;
        Nanos = nanos;
    }

    public static implicit operator decimal(DecimalValue grpcDecimal) =>
        grpcDecimal switch
        {
            null => 0,
            _ => grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor
        };

    public static implicit operator DecimalValue(decimal value)
    {
        var units = decimal.ToInt64(value);
        var nanos = decimal.ToInt32((value - units) * NanoFactor);
        return new DecimalValue(units, nanos);
    }

    public static implicit operator decimal?(DecimalValue? grpcDecimal) =>
        grpcDecimal is null ? null : (decimal)grpcDecimal;

    public static implicit operator DecimalValue?(decimal? value) =>
        value is null ? null : (DecimalValue)value.Value;
}

