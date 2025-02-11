using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate.Entities;

public class TeacherAssignment : Entity<TeacherAssignmentId>
{
    public TeacherAssignment(TeacherAssignmentId id, TeacherId teacherId, CourseId courseId)
        : base(id)
    {
        TeacherId = teacherId;
        CourseId = courseId;
    }

    public TeacherId TeacherId { get; private set; }

    public CourseId CourseId { get; private set; }

    internal static TeacherAssignment Create(TeacherId teacherId, CourseId courseId)
    {
        var teacherAssignment = new TeacherAssignment(
            TeacherAssignmentId.NewTeacherAssignmentId(),
            teacherId,
            courseId
        );
        return teacherAssignment;
    }
}
