using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate;

public class TeacherAssignment : Entity<TeacherAssignmentId>
{
    public TeacherAssignment(
        TeacherAssignmentId id,
        TeacherId teacherId,
        DepartmentId departmentId,
        DateTime dateAssigned
    )
        : base(id)
    {
        TeacherId = teacherId;
        DepartmentId = departmentId;
        DateAssigned = dateAssigned;
    }

    public TeacherId TeacherId { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public DateTime DateAssigned { get; private set; }

    internal static TeacherAssignment Create(
        TeacherId teacherId,
        DepartmentId departmentId,
        DateTime dateAssigned
    )
    {
        var teacherAssignment = new TeacherAssignment(
            TeacherAssignmentId.NewTeacherAssignmentId(),
            teacherId,
            departmentId,
            dateAssigned
        );

        return teacherAssignment;
    }
}
