using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.Entities;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate;

public class Department : AggregateRoot<DepartmentId>
{
    private readonly List<ScheduleEvent> _scheduleEvents = new();
    private readonly List<TeacherAssignment> _teacherAssignments = new();
    private readonly List<StudentEnrollment> _studentEnrollments = new();
    private readonly List<CourseId> _courseIds = new();

    // Private constructor for controlled creation
    private Department(DepartmentId id, DepartmentType departmentType)
        : base(id)
    {
        DepartmentType = departmentType;
    }

    // Properties with private setters for encapsulation
    public DepartmentType DepartmentType { get; private set; }
    public IReadOnlyList<ScheduleEvent> ScheduleEvents => _scheduleEvents.AsReadOnly();
    public IReadOnlyList<TeacherAssignment> TeacherAssignments => _teacherAssignments.AsReadOnly();
    public IReadOnlyList<StudentEnrollment> StudentEnrollments => _studentEnrollments.AsReadOnly();
    public IReadOnlyList<CourseId> CourseIds => _courseIds.AsReadOnly();

    // Factory method with validation
    public static Result<Department> Create(string programName, Level level)
    {
        if (string.IsNullOrWhiteSpace(programName))
        {
            return Result.Failure<Department>(
                Error.Validation("ProgramName.Required", "Program name is required.")
            );
        }

        if (level.Equals(default(Level)))
        {
            return Result.Failure<Department>(
                Error.Validation("Level.Required", "Level is required.")
            );
        }

        var departmentType = new DepartmentType(programName, level);
        var department = new Department(DepartmentId.NewDepartmentId(), departmentType);

        // Optional: Raise a domain event
        // department.Raise(new DepartmentCreatedEvent(department.Id, programName, level));
        return Result.Success(department);
    }

    // Update method with Result and validation
    public Result UpdateDepartment(string programName, Level level)
    {
        if (string.IsNullOrWhiteSpace(programName))
        {
            return Result.Failure(
                Error.Validation("ProgramName.Required", "Program name is required.")
            );
        }

        if (level.Equals(default(Level)))
        {
            return Result.Failure(Error.Validation("Level.Required", "Level is required."));
        }

        DepartmentType = new DepartmentType(programName, level);
        // Optional: Raise a domain event
        // Raise(new DepartmentUpdatedEvent(Id, programName, level));
        return Result.Success();
    }

    public Result AddCourse(CourseId courseId)
    {
        if (courseId is null)
        {
            return Result.Failure(Error.Validation("CourseId.Required", "Course ID is required."));
        }

        if (_courseIds.Contains(courseId))
        {
            return Result.Failure(
                Error.Conflict(
                    "Course.AlreadyAdded",
                    $"Course {courseId} is already added to this department."
                )
            );
        }

        _courseIds.Add(courseId);
        // Raise(new CourseAddedToDepartmentEvent(Id, courseId));
        return Result.Success();
    }

    public Result RemoveCourse(CourseId courseId)
    {
        if (courseId is null)
        {
            return Result.Failure(Error.Validation("CourseId.Required", "Course ID is required."));
        }

        if (!_courseIds.Remove(courseId))
        {
            return Result.Failure(
                Error.NotFound(
                    "Course.NotFound",
                    $"Course {courseId} not found in this department."
                )
            );
        }

        // Raise(new CourseRemovedFromDepartmentEvent(Id, courseId));
        return Result.Success();
    }

    public Result AddScheduleEvent(DateTime date, string description)
    {
        if (date < DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation("Date.Invalid", "Schedule event date must be in the future.")
            );
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure(
                Error.Validation("Description.Required", "Description is required.")
            );
        }

        var scheduleEvent = ScheduleEvent.Create(Id, date, description);
        _scheduleEvents.Add(scheduleEvent);
        // Raise(new ScheduleEventAddedEvent(Id, scheduleEvent.Id, date, description));
        return Result.Success();
    }

    public Result RemoveScheduleEvent(ScheduleEventId scheduleEventId)
    {
        if (scheduleEventId is null)
        {
            return Result.Failure(
                Error.Validation("ScheduleEventId.Required", "Schedule event ID is required.")
            );
        }

        Result<ScheduleEvent> scheduleEvent = _scheduleEvents.FirstOrDefault(se =>
            se.Id == scheduleEventId
        );
        if (scheduleEvent is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "ScheduleEvent.NotFound",
                    $"Schedule event {scheduleEventId} not found."
                )
            );
        }

        _scheduleEvents.Remove(scheduleEvent.Value);
        // Raise(new ScheduleEventRemovedEvent(Id, scheduleEventId));
        return Result.Success();
    }

    public Result AddTeacherAssignment(TeacherId teacherId, DateTime dateAssigned)
    {
        if (teacherId is null)
        {
            return Result.Failure(
                Error.Validation("TeacherId.Required", "Teacher ID is required.")
            );
        }

        if (dateAssigned > DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation("DateAssigned.Invalid", "Assignment date cannot be in the future.")
            );
        }

        if (_teacherAssignments.Any(ta => ta.TeacherId == teacherId))
        {
            return Result.Failure(
                Error.Conflict(
                    "Teacher.AlreadyAssigned",
                    $"Teacher {teacherId} is already assigned."
                )
            );
        }

        var teacherAssignment = TeacherAssignment.Create(teacherId, Id, dateAssigned);
        _teacherAssignments.Add(teacherAssignment);
        // Raise(new TeacherAssignedToDepartmentEvent(Id, teacherId, dateAssigned));
        return Result.Success();
    }

    public Result RemoveTeacherAssignment(TeacherId teacherId)
    {
        if (teacherId is null)
        {
            return Result.Failure(
                Error.Validation("TeacherId.Required", "Teacher ID is required.")
            );
        }

        Result<TeacherAssignment> assignment = _teacherAssignments.FirstOrDefault(ta =>
            ta.TeacherId == teacherId
        );
        if (assignment == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "TeacherAssignment.NotFound",
                    $"Teacher {teacherId} not assigned to this department."
                )
            );
        }

        _teacherAssignments.Remove(assignment.Value);
        // Raise(new TeacherRemovedFromDepartmentEvent(Id, teacherId));
        return Result.Success();
    }

    // Assuming StudentEnrollment follows a similar pattern
    public Result AddStudentEnrollment(StudentId studentId, DateTime enrollmentDate)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (enrollmentDate > DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation(
                    "EnrollmentDate.Invalid",
                    "Enrollment date cannot be in the future."
                )
            );
        }

        if (_studentEnrollments.Any(se => se.StudentId == studentId))
        {
            return Result.Failure(
                Error.Conflict(
                    "Student.AlreadyEnrolled",
                    $"Student {studentId} is already enrolled."
                )
            );
        }

        var enrollment = StudentEnrollment.Create(studentId, Id, enrollmentDate);
        _studentEnrollments.Add(enrollment);
        // Raise(new StudentEnrolledInDepartmentEvent(Id, studentId, enrollmentDate));
        return Result.Success();
    }

    public Result RemoveStudentEnrollment(StudentId studentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        Result<StudentEnrollment> enrollment = _studentEnrollments.FirstOrDefault(se =>
            se.StudentId == studentId
        );
        if (enrollment == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "StudentEnrollment.NotFound",
                    $"Student {studentId} not enrolled in this department."
                )
            );
        }

        _studentEnrollments.Remove(enrollment.Value);
        // Raise(new StudentRemovedFromDepartmentEvent(Id, studentId));
        return Result.Success();
    }
}
