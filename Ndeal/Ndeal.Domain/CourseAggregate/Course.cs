using Ndeal.Domain.CourseAggregate.Entities;
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate;

public class Course : AggregateRoot<CourseId>
{
    private readonly List<CourseOutline> _courseOutlines = new();
    private readonly List<Assignment> _assignments = new();
    private readonly List<StudentRegistration> _studentRegistrations = new();
    private readonly List<TeacherAssignment> _teacherAssignments = new();

    // Private constructor for creation via factory method
    private Course(
        CourseId id,
        DepartmentId departmentId,
        string courseCode,
        string courseName,
        CourseUnitRating courseUnitRating
    )
        : base(id)
    {
        CourseCode = courseCode;
        DepartmentId = departmentId;
        CourseName = courseName;
        CourseUnitRating = courseUnitRating;
    }

    // Properties with private setters for encapsulation
    public string CourseCode { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public string CourseName { get; private set; }
    public CourseUnitRating CourseUnitRating { get; private set; }
    public IReadOnlyList<CourseOutline> CourseOutlines => _courseOutlines.AsReadOnly();
    public IReadOnlyList<Assignment> Assignments => _assignments.AsReadOnly();
    public IReadOnlyList<StudentRegistration> StudentRegistrations =>
        _studentRegistrations.AsReadOnly();
    public IReadOnlyList<TeacherAssignment> TeacherAssignments => _teacherAssignments.AsReadOnly();

    // Factory method with validation
    public static Result<Course> Create(
        string courseCode,
        string courseName,
        DepartmentId departmentId,
        CourseUnitRating courseUnitRating
    )
    {
        // Validation
        if (string.IsNullOrWhiteSpace(courseCode))
        {
            return Result.Failure<Course>(
                Error.Validation("CourseCode.Required", "Course code is required.")
            );
        }

        if (string.IsNullOrWhiteSpace(courseName))
        {
            return Result.Failure<Course>(
                Error.Validation("CourseName.Required", "Course name is required.")
            );
        }

        if (departmentId is null)
        {
            return Result.Failure<Course>(
                Error.Validation("DepartmentId.Required", "Department ID is required.")
            );
        }

        if (courseUnitRating is null)
        {
            return Result.Failure<Course>(
                Error.Validation("CourseUnitRating.Required", "Course unit rating is required.")
            );
        }

        var course = new Course(
            CourseId.NewCourseId(),
            departmentId,
            courseCode,
            courseName,
            courseUnitRating
        );

        // Optional: Raise a domain event
        // course.Raise(new CourseCreatedEvent(course.Id, courseCode, courseName));
        return Result.Success(course);
    }

    // Update method with Result and validation
    public Result UpdateCourse(
        string courseCode,
        string courseName,
        DepartmentId departmentId,
        CourseUnitRating courseUnitRating
    )
    {
        if (string.IsNullOrWhiteSpace(courseCode))
        {
            return Result.Failure(
                Error.Validation("CourseCode.Required", "Course code is required.")
            );
        }

        if (string.IsNullOrWhiteSpace(courseName))
        {
            return Result.Failure(
                Error.Validation("CourseName.Required", "Course name is required.")
            );
        }

        if (departmentId is null)
        {
            return Result.Failure(
                Error.Validation("DepartmentId.Required", "Department ID is required.")
            );
        }

        if (courseUnitRating is null)
        {
            return Result.Failure(
                Error.Validation("CourseUnitRating.Required", "Course unit rating is required.")
            );
        }

        CourseCode = courseCode;
        DepartmentId = departmentId;
        CourseName = courseName;
        CourseUnitRating = courseUnitRating;

        // Optional: Raise a domain event
        // Raise(new CourseUpdatedEvent(Id, courseCode, courseName));
        return Result.Success();
    }

    public Result AddCourseOutline(string title, string description, string duration)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure(
                Error.Validation("Title.Required", "Course outline title is required.")
            );
        }

        if (_courseOutlines.Any(co => co.CourseOutlineTitle == title))
        {
            return Result.Failure(
                Error.Conflict(
                    "CourseOutline.Duplicate",
                    $"A course outline with title '{title}' already exists."
                )
            );
        }

        Result<CourseOutline> courseOutlineResult = CourseOutline.Create(
            Id,
            title,
            description,
            duration
        );
        if (courseOutlineResult.IsFailure)
        {
            return courseOutlineResult; // Propagate creation failure
        }

        _courseOutlines.Add(courseOutlineResult.Value);
        // Raise(new CourseOutlineAddedEvent(Id, courseOutlineResult.Value.Id, title));
        return Result.Success();
    }

    public Result RemoveCourseOutline(CourseOutlineId courseOutlineId)
    {
        Result<CourseOutline> courseOutline = _courseOutlines.SingleOrDefault(co =>
            co.Id == courseOutlineId
        );
        if (courseOutline is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "CourseOutline.NotFound",
                    $"Course outline with ID {courseOutlineId} not found."
                )
            );
        }

        _courseOutlines.Remove(courseOutline.Value);
        // Raise(new CourseOutlineRemovedEvent(Id, courseOutlineId));
        return Result.Success();
    }

    public Result AddAssignment(
        string title,
        string description,
        DateTime dueDate,
        TeacherId teacherId,
        int maxScore
    )
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure(
                Error.Validation("Title.Required", "Assignment title is required.")
            );
        }

        if (dueDate < DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation("DueDate.Invalid", "Due date must be in the future.")
            );
        }

        if (maxScore <= 0)
        {
            return Result.Failure(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (teacherId is null)
        {
            return Result.Failure(
                Error.Validation("TeacherId.Required", "Teacher ID is required.")
            );
        }

        Result<Assignment> assignmentResult = Assignment.Create(
            title,
            description,
            dueDate,
            Id,
            teacherId,
            maxScore
        );
        if (assignmentResult.IsFailure)
        {
            return assignmentResult;
        }

        _assignments.Add(assignmentResult.Value);
        // Raise(new AssignmentAddedEvent(Id, assignmentResult.Value.Id, title));
        return Result.Success();
    }

    public Result RegisterStudent(StudentId studentId, DepartmentId departmentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (departmentId is null)
        {
            return Result.Failure(
                Error.Validation("DepartmentId.Required", "Department ID is required.")
            );
        }

        if (_studentRegistrations.Any(sr => sr.StudentId == studentId))
        {
            return Result.Failure(
                Error.Conflict(
                    "Student.AlreadyRegistered",
                    $"Student {studentId} is already registered."
                )
            );
        }

        var registration = StudentRegistration.Create(studentId, Id, departmentId);
        _studentRegistrations.Add(registration);
        // Raise(new StudentRegisteredEvent(Id, studentId));
        return Result.Success();
    }

    public Result AssignTeacher(TeacherId teacherId)
    {
        if (teacherId is null)
        {
            return Result.Failure(
                Error.Validation("TeacherId.Required", "Teacher ID is required.")
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

        Result<TeacherAssignment> teacherAssignmentResult = TeacherAssignment.Create(teacherId, Id);
        if (teacherAssignmentResult.IsFailure)
        {
            return teacherAssignmentResult;
        }

        _teacherAssignments.Add(teacherAssignmentResult.Value);
        // Raise(new TeacherAssignedEvent(Id, teacherId));
        return Result.Success();
    }

    public Result RemoveStudent(StudentId studentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        Result<StudentRegistration> registration = _studentRegistrations.SingleOrDefault(sr =>
            sr.StudentId == studentId
        );
        if (registration == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Student.NotFound",
                    $"Student {studentId} not registered in this course."
                )
            );
        }

        _studentRegistrations.Remove(registration.Value);
        // Raise(new StudentRemovedEvent(Id, studentId));
        return Result.Success();
    }

    public Result RemoveTeacher(TeacherId teacherId)
    {
        if (teacherId is null)
        {
            return Result.Failure(
                Error.Validation("TeacherId.Required", "Teacher ID is required.")
            );
        }

        Result<TeacherAssignment> assignment = _teacherAssignments.SingleOrDefault(ta =>
            ta.TeacherId == teacherId
        );
        if (assignment == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Teacher.NotFound",
                    $"Teacher {teacherId} not assigned to this course."
                )
            );
        }

        _teacherAssignments.Remove(assignment.Value);
        // Raise(new TeacherRemovedEvent(Id, teacherId));
        return Result.Success();
    }

    public Result AddAssignmentScoreToAssignment(
        StudentId studentId,
        AssignmentId assignmentId,
        int score
    )
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (assignmentId is null)
        {
            return Result.Failure(
                Error.Validation("AssignmentId.Required", "Assignment ID is required.")
            );
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        Assignment? assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId);
        if (assignment == null)
        {
            return Result.Failure(
                Error.NotFound("Assignment.NotFound", $"Assignment {assignmentId} not found.")
            );
        }

        Result<AssignmentScore> scoreResult = AssignmentScore.Create(assignment, studentId, score);
        if (scoreResult.IsFailure)
        {
            return scoreResult;
        }

        assignment.AddAssignmentScore(scoreResult.Value);
        // Raise(new AssignmentScoreAddedEvent(Id, assignmentId, studentId, score));
        return Result.Success();
    }

    public Result UpdateAssignmentScore(StudentId studentId, AssignmentId assignmentId, int score)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (assignmentId is null)
        {
            return Result.Failure(
                Error.Validation("AssignmentId.Required", "Assignment ID is required.")
            );
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        Assignment? assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId);
        if (assignment == null)
        {
            return Result.Failure(
                Error.NotFound("Assignment.NotFound", $"Assignment {assignmentId} not found.")
            );
        }

        AssignmentScore? assignmentScore = assignment.AssignmentScores.FirstOrDefault(s =>
            s.StudentId == studentId
        );
        if (assignmentScore == null)
        {
            return Result.Failure(
                Error.NotFound("Score.NotFound", $"Score for student {studentId} not found.")
            );
        }

        assignmentScore.UpdateScore(score);
        // Raise(new AssignmentScoreUpdatedEvent(Id, assignmentId, studentId, score));
        return Result.Success();
    }

    public Result RemoveAssignmentScore(StudentId studentId, AssignmentId assignmentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (assignmentId is null)
        {
            return Result.Failure(
                Error.Validation("AssignmentId.Required", "Assignment ID is required.")
            );
        }

        Assignment? assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId);
        if (assignment == null)
        {
            return Result.Failure(
                Error.NotFound("Assignment.NotFound", $"Assignment {assignmentId} not found.")
            );
        }

        AssignmentScore? assignmentScore = assignment.AssignmentScores.FirstOrDefault(s =>
            s.StudentId == studentId
        );
        if (assignmentScore == null)
        {
            return Result.Failure(
                Error.NotFound("Score.NotFound", $"Score for student {studentId} not found.")
            );
        }

        assignment.RemoveAssignmentScore(assignmentScore);
        // Raise(new AssignmentScoreRemovedEvent(Id, assignmentId, studentId));
        return Result.Success();
    }
}
