using Ndeal.Domain.CourseSessionAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseSessionAggregate.Entities;

public class Attendance : Entity<AttendanceId>
{
    public StudentId StudentId { get; private set; }
    public CourseSessionId CourseSessionId { get; private set; }
    public AttendanceRecord AttendanceRecord { get; private set; }

    private Attendance(
        AttendanceId id,
        StudentId studentId,
        CourseSessionId courseSessionId,
        AttendanceRecord attendanceRecord
    )
        : base(id)
    {
        StudentId = studentId;
        CourseSessionId = courseSessionId;
        AttendanceRecord = attendanceRecord;
    }

    public static Attendance Create(
        StudentId studentId,
        CourseSessionId courseSessionId,
        DateTime date,
        AttendanceRecord.AttendanceStatus status
    )
    {
        var attendanceRecord = new AttendanceRecord(date, status);
        return new Attendance(
            AttendanceId.NewAttendanceId(),
            studentId,
            courseSessionId,
            attendanceRecord
        );
    }

    public void UpdateStatus(AttendanceRecord.AttendanceStatus newStatus)
    {
        AttendanceRecord = new AttendanceRecord(AttendanceRecord.Date, newStatus); // Create a new AttendanceRecord
    }
}
