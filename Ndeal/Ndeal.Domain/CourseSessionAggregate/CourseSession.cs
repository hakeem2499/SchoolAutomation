using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.CourseSessionAggregate.Entities;
using Ndeal.Domain.CourseSessionAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseSessionAggregate;

public class CourseSession : AggregateRoot<CourseSessionId>
{
    public CourseSession(CourseSessionId id, CourseId courseId, DateTime date, string location)
        : base(id)
    {
        CourseId = courseId;
        Date = date.Date; // Store only the date part (time is irrelevant for course sessions)
        Location = location;
    }

    private readonly List<Attendance> _attendances = new();
    public CourseId CourseId { get; }
    public DateTime Date { get; }
    public string Location { get; }
    public IReadOnlyList<Attendance> Attendances => _attendances;

    public void MarkAttendance(StudentId studentId, AttendanceRecord.AttendanceStatus status)
    {
        Attendance? attendance = _attendances.FirstOrDefault(a => a.StudentId == studentId);
        if (attendance == null)
        {
            attendance = Attendance.Create(studentId, Id, Date, status); // Use factory method
            _attendances.Add(attendance);
            //Raise(new StudentAttendanceMarkedEvent(Id, studentId, status)); // Raise Domain Event
        }
        else
        {
            attendance.UpdateStatus(status); // Update existing attendance
            //Raise(new StudentAttendanceMarkedEvent(Id, studentId, status)); // Raise Domain Event
        }
    }
}
