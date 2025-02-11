using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate.Entities;

public class StudentRegistration : Entity<StudentRegistrationId>
{
    public StudentRegistration(
        StudentRegistrationId id,
        StudentId studentId,
        DepartmentId departmentId,
        CourseId courseId
    )
        : base(id)
    {
        StudentId = studentId;
        CourseId = courseId;
        DepartmentId = departmentId;
        CourseId = courseId;
    }

    public StudentId StudentId { get; private set; }
    public CourseId CourseId { get; private set; }
    public DepartmentId DepartmentId { get; private set; }

    internal static StudentRegistration Create(
        StudentId studentId,
        CourseId courseId,
        DepartmentId departmentId
    )
    {
        var studentRegistration = new StudentRegistration(
            StudentRegistrationId.NewStudentRegistrationId(),
            studentId,
            departmentId,
            courseId
        );

        return studentRegistration;
    }
}
