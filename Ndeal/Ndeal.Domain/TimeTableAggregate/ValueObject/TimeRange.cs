using System.Globalization;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.TimeTableAggregate.ValueObjects;

public class TimeRange : ValueObject
{
    public DayOfWeek DayOfWeek { get; }
    public TimeSpan StartTime { get; }
    public TimeSpan EndTime { get; }

    public TimeRange(DayOfWeek dayOfWeek, string startTime, string endTime)
    {
        if (
            !TryParseTime(startTime, out TimeSpan start) || !TryParseTime(endTime, out TimeSpan end)
        )
        {
            throw new ArgumentException(
                "Invalid time format. Use HH:mm or HH:mm tt (e.g., 08:00 or 8:00 AM)."
            );
        }

        if (start > end)
        {
            throw new ArgumentException("Start time cannot be after end time.");
        }

        DayOfWeek = dayOfWeek;
        StartTime = start;
        EndTime = end;
    }

    private static bool TryParseTime(string time, out TimeSpan parsedTime)
    {
        return TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out parsedTime)
            || TimeSpan.TryParseExact(
                time,
                "hh\\:mm tt",
                CultureInfo.InvariantCulture,
                out parsedTime
            );
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DayOfWeek;
        yield return StartTime;
        yield return EndTime;
    }

    public override string ToString()
    {
        return $"{DayOfWeek}: {StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }

    public bool Overlaps(TimeRange other)
    {
        if (DayOfWeek != other.DayOfWeek)
        {
            return false; // Check day of the week first
        }

        return StartTime < other.EndTime && EndTime > other.StartTime;
    }

    public bool Contains(TimeRange other)
    {
        if (DayOfWeek != other.DayOfWeek)
        {
            return false;
        }

        return StartTime <= other.StartTime && EndTime >= other.EndTime;
    }

    public bool IsWithin(TimeRange other)
    {
        if (DayOfWeek != other.DayOfWeek)
        {
            return false;
        }

        return other.StartTime <= StartTime && other.EndTime >= EndTime;
    }

    public static TimeRange FromTimes(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        return new TimeRange(dayOfWeek, startTime.ToString(@"hh\:mm"), endTime.ToString(@"hh\:mm"));
    }
}
