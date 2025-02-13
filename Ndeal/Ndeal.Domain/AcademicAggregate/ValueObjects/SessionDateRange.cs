using Ndeal.SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.ValueObjects;

public class MonthYear : ValueObject
{
    public int Year { get; }
    public int Month { get; }

    public MonthYear(int year, int month)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(year, 1900);

        if (month < 1 || month > 12) // Month validation
        {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        }

        Year = year;
        Month = month;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
        yield return Month;
    }

    public override string ToString()
    {
        return $"{Month}/{Year}"; // Or your preferred format (e.g., "January 2024")
    }

    // Optional: Methods for comparing MonthYear objects (if needed)
    public bool IsSameMonthYearAs(MonthYear other) => Year == other.Year && Month == other.Month;

    public bool IsBefore(MonthYear other) =>
        Year < other.Year || Year == other.Year && Month < other.Month;

    public bool IsAfter(MonthYear other) =>
        Year > other.Year || Year == other.Year && Month > other.Month;

    // Optional: Create from DateTime (if you need this conversion)
    public static MonthYear FromDateTime(DateTime dateTime) => new(dateTime.Year, dateTime.Month);

    // Optional: Get DateTime of first day of the month/year
    public DateTime FirstDayOfMonth() => new(Year, Month, 1, 0, 0, 0, DateTimeKind.Utc);

    // Optional: Get DateTime of last day of the month/year
    public DateTime LastDayOfMonth() =>
        new(Year, Month, DateTime.DaysInMonth(Year, Month), 0, 0, 0, DateTimeKind.Utc);
}
