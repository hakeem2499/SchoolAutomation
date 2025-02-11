using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.Entities;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate;

public class Department : AggregateRoot<DepartmentId>
{
    private readonly List<ScheduleEvent> _scheduleEvents = new();
    private readonly List<TeacherAssignment> _teacherAssignments = new();
    private readonly List<StudentEnrollment> _studentEnrollments = new();

    private readonly List<CourseId> _courseIds = new();

    public Department(DepartmentId id, DepartmentType departmentType)
        : base(id)
    {
        DepartmentType = departmentType;
    }

    public DepartmentType DepartmentType { get; private set; }

    public IReadOnlyList<ScheduleEvent> ScheduleEvents => _scheduleEvents.AsReadOnly();
    public IReadOnlyList<TeacherAssignment> TeacherAssignments => _teacherAssignments.AsReadOnly();
    public IReadOnlyList<StudentEnrollment> StudentEnrollments => _studentEnrollments.AsReadOnly();
    public IReadOnlyList<CourseId> CourseIds => _courseIds.AsReadOnly();

    public static Department Create(string programName, Level level)
    {
        var department = new Department(
            DepartmentId.NewDepartmentId(),
            new DepartmentType(programName, level)
        );

        return department;
    }

    public void AddCourse(CourseId courseId)
    {
        _courseIds.Add(courseId);
    }

    public void AddScheduleEvent(DepartmentId departmentId, DateTime date, string description)
    {
        var scheduleEvent = ScheduleEvent.Create(departmentId, date, description);

        _scheduleEvents.Add(scheduleEvent);
    }

    public void AddTeacherAssignment(
        TeacherId teacherId,
        DepartmentId departmentId,
        DateTime dateAssigned
    )
    {
        var teacherAssignment = TeacherAssignment.Create(teacherId, departmentId, dateAssigned);

        _teacherAssignments.Add(teacherAssignment);
    }
}
