using Ndeal.Domain.AttendanceAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.AttendanceAggregate.Entities;

public class StudentAttendance : Entity<StudentAttendanceId>
{
    public StudentAttendance(
        StudentAttendanceId id,
        StudentId studentId,
        Attendance attendance,
        AttendanceRecord attendanceRecord
    )
        : base(id)
    {
        StudentId = studentId;
        Attendance = attendance;
        AttendanceRecord = attendanceRecord;
    }

    public StudentId StudentId { get; private set; }
    public Attendance Attendance { get; private set; }
    public AttendanceRecord AttendanceRecord { get; private set; }
    

    internal static StudentAttendance Create(
        StudentId studentId,
        Attendance attendance,
        AttendanceRecord attendanceRecord
    )
    {
        var studentAttendance = new StudentAttendance(
            StudentAttendanceId.NewStudentAttendanceId(),
            studentId,
            attendance,
            attendanceRecord
        );

        return studentAttendance;
    }
}
