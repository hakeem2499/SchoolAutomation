// Domain/CourseSessionAggregate/CourseSession.cs
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.CourseSessionAggregate.Entities;
using Ndeal.Domain.CourseSessionAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.CourseSessionAggregate;

public sealed class CourseSession : AggregateRoot<CourseSessionId>
{
    private readonly List<Attendance> _attendances = new();

    private CourseSession(CourseSessionId id, CourseId courseId, DateTime date, string location)
        : base(id)
    {
        CourseId = courseId;
        Date = date.Date;
        Location = location;
    }

    public CourseId CourseId { get; private set; }
    public DateTime Date { get; private set; }
    public string Location { get; private set; }
    public IReadOnlyList<Attendance> Attendances => _attendances.AsReadOnly();

    public static Result<CourseSession> Create(CourseId courseId, DateTime date, string location)
    {
        if (courseId is null)
        {
            return Result.Failure<CourseSession>(
                Error.Validation("CourseId.Required", "Course ID is required.")
            );
        }

        if (date < DateTime.UtcNow.Date)
        {
            return Result.Failure<CourseSession>(
                Error.Validation("Date.Invalid", "Session date cannot be in the past.")
            );
        }

        if (string.IsNullOrWhiteSpace(location))
        {
            return Result.Failure<CourseSession>(
                Error.Validation("Location.Required", "Location is required.")
            );
        }

        return Result.Success(
            new CourseSession(CourseSessionId.NewCourseSessionId(), courseId, date, location)
        );
    }

    public Result MarkAttendance(StudentId studentId, Attendance.AttendanceStatus status)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (!Enum.IsDefined(typeof(Attendance.AttendanceStatus), status))
        {
            return Result.Failure(Error.Validation("Status.Invalid", "Invalid attendance status."));
        }

        Attendance? existingAttendance = _attendances.FirstOrDefault(a => a.StudentId == studentId);
        if (existingAttendance == null)
        {
            Result<Attendance> attendanceResult = Attendance.Create(studentId, Id, Date, status);
            if (attendanceResult.IsFailure)
            {
                return attendanceResult;
            }

            _attendances.Add(attendanceResult.Value);
        }
        else
        {
            existingAttendance.UpdateStatus(status);
        }

        return Result.Success();
    }
}
