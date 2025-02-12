using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate.Entities;

public class TeacherAssignment : Entity<TeacherAssignmentId>
{
    public TeacherAssignment(TeacherAssignmentId id, TeacherId teacherId, Course course)
        : base(id)
    {
        TeacherId = teacherId;
        Course = course;
    }

    public TeacherId TeacherId { get; private set; }

    public Course Course { get; private set; }
    public CourseId CourseId => Course.Id;

    internal static TeacherAssignment Create(TeacherId teacherId, Course course)
    {
        var teacherAssignment = new TeacherAssignment(
            TeacherAssignmentId.NewTeacherAssignmentId(),
            teacherId,
            course
        );
        return teacherAssignment;
    }
}
