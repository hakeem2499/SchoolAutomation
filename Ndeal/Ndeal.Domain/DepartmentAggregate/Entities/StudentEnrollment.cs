using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate;

public class StudentEnrollment : Entity<StudentEnrollmentId>
{
    public StudentEnrollment(
        StudentEnrollmentId id,
        StudentId studentId,
        DepartmentId departmentId,
        DateTime dateEnrolled
    )
        : base(id)
    {
        StudentId = studentId;
        DepartmentId = departmentId;
        DateEnrolled = dateEnrolled;
    }

    public StudentId StudentId { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public DateTime DateEnrolled { get; private set; }

    internal static StudentEnrollment Create(
        StudentId studentId,
        DepartmentId departmentId,
        DateTime dateEnrolled
    )
    {
        var studentEnrollment = new StudentEnrollment(
            StudentEnrollmentId.NewStudentEnrollmentId(),
            studentId,
            departmentId,
            dateEnrolled
        );

        return studentEnrollment;
    }
}