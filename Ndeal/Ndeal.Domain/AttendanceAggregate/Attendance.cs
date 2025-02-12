using Ndeal.Domain.AttendanceAggregate.Entities;
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.AttendanceAggregate.ValueObjects;

public class Attendance : AggregateRoot<AttendanceId>
{
    private readonly List<StudentAttendance> studentAttendances = new();

    public Attendance(AttendanceId id, CourseId courseId, TeacherId teacherId, DateTime date)
        : base(id)
    {
        CourseId = courseId;
        TeacherId = teacherId;
        Date = date;
    }

    public CourseId CourseId { get; private set; }
    public TeacherId TeacherId { get; private set; }
    public DateTime Date { get; private set; }
    public IReadOnlyList<StudentAttendance> StudentAttendances => studentAttendances.AsReadOnly();

    public static Attendance Create(CourseId courseId, TeacherId teacherId, DateTime? date)
    {
        var attendance = new Attendance(
            AttendanceId.NewAttendanceId(),
            courseId,
            teacherId,
            date ?? DateTime.Now
        );
        return attendance;
    }

    public void PresentStudentAttendance(StudentId studentId)
    {
        var attendanceRecord = AttendanceRecord.Present(DateTime.Now);
        var studentAttendance = StudentAttendance.Create(studentId, this, attendanceRecord);
        studentAttendances.Add(studentAttendance);
    }

    public void AbsentStudentAttendance(StudentId studentId)
    {
        var attendanceRecord = AttendanceRecord.Absent(DateTime.Now);
        var studentAttendance = StudentAttendance.Create(studentId, this, attendanceRecord);
        studentAttendances.Add(studentAttendance);
    }

    
}
