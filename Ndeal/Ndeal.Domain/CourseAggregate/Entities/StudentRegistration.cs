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
        Course course
    )
        : base(id)
    {
        StudentId = studentId;
        Course = course;
        DepartmentId = departmentId;
    }

    public StudentId StudentId { get; private set; }
    public Course Course { get; private set; }
    public CourseId CourseId => Course.Id;
    public DepartmentId DepartmentId { get; private set; }

    internal static StudentRegistration Create(
        StudentId studentId,
        Course course,
        DepartmentId departmentId
    )
    {
        var studentRegistration = new StudentRegistration(
            StudentRegistrationId.NewStudentRegistrationId(),
            studentId,
            departmentId,
            course
        );

        return studentRegistration;
    }
}
