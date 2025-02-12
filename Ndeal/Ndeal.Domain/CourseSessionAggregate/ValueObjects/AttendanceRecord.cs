using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseSessionAggregate.ValueObjects;

public class AttendanceRecord : ValueObject
{
    public DateTime Date { get; }
    public AttendanceStatus Status { get; } // Use an enum for attendance status

    public AttendanceRecord(DateTime date, AttendanceStatus status)
    {
        Date = date.Date; // Store only the date part (time is irrelevant for attendance records)
        Status = status;
    }

    public enum AttendanceStatus
    {
        Present,
        Absent,
        Late,
        Excused, // Add other statuses as needed (e.g., Tardy, HalfDay)
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Date;
        yield return Status;
    }

    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd}: {Status}"; // Example: "2024-10-27: Present"
    }

    // Optional: Factory methods for easier creation
    public static AttendanceRecord Present(DateTime date) => new(date, AttendanceStatus.Present);

    public static AttendanceRecord Absent(DateTime date) => new(date, AttendanceStatus.Absent);

    public static AttendanceRecord Late(DateTime date) => new(date, AttendanceStatus.Late);

    public static AttendanceRecord Excused(DateTime date) => new(date, AttendanceStatus.Excused);

    // Optional: Methods for comparing dates (if needed)
    public bool IsOnSameDateAs(DateTime otherDate) => Date == otherDate.Date;
}
