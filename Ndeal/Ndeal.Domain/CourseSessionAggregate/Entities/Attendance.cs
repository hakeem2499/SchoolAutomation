// Domain/CourseSessionAggregate/Entities/Attendance.cs
using Ndeal.Domain.CourseSessionAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.CourseSessionAggregate.Entities;

public class Attendance : Entity<AttendanceId>
{
    public StudentId StudentId { get; private set; }
    public CourseSessionId CourseSessionId { get; private set; }
    public DateTime Date { get; private set; }
    public AttendanceStatus Status { get; private set; }

    private Attendance(
        AttendanceId id,
        StudentId studentId,
        CourseSessionId courseSessionId,
        DateTime date,
        AttendanceStatus status
    )
        : base(id)
    {
        StudentId = studentId;
        CourseSessionId = courseSessionId;
        Date = date.Date;
        Status = status;
    }

    public enum AttendanceStatus
    {
        Present,
        Absent,
        Late,
        Excused,
    }

    public static Result<Attendance> Create(
        StudentId studentId,
        CourseSessionId courseSessionId,
        DateTime date,
        AttendanceStatus status
    )
    {
        if (studentId is null)
        {
            return Result.Failure<Attendance>(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (courseSessionId is null)
        {
            return Result.Failure<Attendance>(
                Error.Validation("CourseSessionId.Required", "Course session ID is required.")
            );
        }

        if (!Enum.IsDefined(typeof(AttendanceStatus), status))
        {
            return Result.Failure<Attendance>(
                Error.Validation("Status.Invalid", "Invalid attendance status.")
            );
        }

        var attendance = new Attendance(
            AttendanceId.NewAttendanceId(),
            studentId,
            courseSessionId,
            date,
            status
        );
        return Result.Success(attendance);
    }

    public void UpdateStatus(AttendanceStatus status)
    {
        if (!Enum.IsDefined(typeof(AttendanceStatus), status))
        {
            throw new ArgumentException("Invalid attendance status.", nameof(status));
        }

        Status = status;
    }
}
