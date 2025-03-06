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

    public Course(
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

    public string CourseCode { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public string CourseName { get; private set; }
    public CourseUnitRating CourseUnitRating { get; private set; }
    public IReadOnlyList<CourseOutline> CourseOutlines => _courseOutlines.AsReadOnly();
    public IReadOnlyList<Assignment> Assignments => _assignments.AsReadOnly();
    public IReadOnlyList<StudentRegistration> StudentRegistrations =>
        _studentRegistrations.AsReadOnly();
    public IReadOnlyList<TeacherAssignment> TeacherAssignments => _teacherAssignments.AsReadOnly();

    public static Course Create(
        string courseCode,
        string courseName,
        DepartmentId departmentId,
        CourseUnitRating courseUnitRating
    )
    {
        var course = new Course(
            CourseId.NewCourseId(),
            departmentId,
            courseCode,
            courseName,
            courseUnitRating
        );

        return course;
    }

    // This is a method for updating a course
    public void UpdateCourse(
        string courseCode,
        string courseName,
        DepartmentId departmentId,
        CourseUnitRating courseUnitRating
    )
    {
        CourseCode = courseCode;
        DepartmentId = departmentId;
        CourseName = courseName;
        CourseUnitRating = courseUnitRating;
    }

    public Result AddCourseOutline(string title, string description, string duration)
    {
        // Add validation here (e.g., check for duplicate titles, valid duration format)
        if (_courseOutlines.Any(co => co.CourseOutlineTitle == title))
        {
            Result.Failure(
                Error.Conflict(
                    "CourseOutline.AlreadyExists",
                    "A course with this course outline already exists"
                )
            );
        }

        Result<CourseOutline> courseOutline = CourseOutline.Create(
            Id,
            title,
            description,
            duration
        ); //  pass courseId directly which is gotten from then entity class
        _courseOutlines.Add(courseOutline.Value);
        return Result.Success();

        //Raise(new CourseOutlineAddedEvent(Id, courseOutline.Id, title)); // Raise Domain Event
    }

    public Result RemoveCourseOutline(CourseOutlineId courseOutlineId)
    {
        CourseOutline? courseOutline = _courseOutlines.SingleOrDefault(co =>
            co.Id == courseOutlineId
        ); // Or RemoveAll if needed
        if (courseOutline != null)
        {
            _courseOutlines.Remove(courseOutline);
            //Raise(new CourseOutlineRemovedEvent(Id, courseOutlineId)); // Raise Domain Event
        }
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
        // Add validation (e.g., maxScore > 0, dueDate in the future)

        Result<Assignment> assignment = Assignment.Create(
            title,
            description,
            dueDate,
            Id,
            teacherId,
            maxScore
        ); // No need to pass courseId
        _assignments.Add(assignment.Value);
        return Result.Success();
    }

    // ... other methods (similar improvements)

    public void RegisterStudent(StudentId studentId, DepartmentId departmentId)
    {
        // ... any other validation logic (e.g., checking prerequisites, course capacity)

        var studentRegistration = StudentRegistration.Create(studentId, Id, departmentId); // Use this.Id
        _studentRegistrations.Add(studentRegistration);

        //Raise(new StudentRegisteredEvent(Id, studentId)); // Raise Domain Event
    }

    public Result AssignTeacher(TeacherId teacherId)
    {
        Result<TeacherAssignment> teacherAssignment = TeacherAssignment.Create(teacherId, Id); // Use this.Id
        _teacherAssignments.Add(teacherAssignment.Value);
        return Result.Success();

        // Raise(new TeacherAssignedEvent(Id, teacherId)); // Raise Domain Event
    }

    // ... other methods

    // // Example Domain Events
    // public record CourseOutlineAddedEvent(
    //     CourseId CourseId,
    //     CourseOutlineId CourseOutlineId,
    //     string Title
    // ) : IDomainEvent;

    public Result RemoveStudent(StudentId studentId)
    {
        StudentRegistration? studentRegistration = _studentRegistrations.SingleOrDefault(sr =>
            sr.StudentId == studentId
        );
        if (studentRegistration is null)
        {
            return Result.Failure(
                Error.NotFound("Student.Notfound", "This student is not found in the database")
            );
        }
        _studentRegistrations.Remove(studentRegistration);
        return Result.Success();
    }

    // This is a method for removing a teacher from a course
    public void RemoveTeacher(TeacherId teacherId)
    {
        TeacherAssignment? teacherAssignment = _teacherAssignments.SingleOrDefault(ta =>
            ta.TeacherId == teacherId
        );
        if (teacherAssignment is not null)
        {
            _teacherAssignments.Remove(teacherAssignment);
        }
    }

    // For assignment Scores

    public Result AddAssignmentScoreToAssignment(
        StudentId studentId,
        AssignmentId assignmentId,
        int score
    )
    {
        Assignment? assignment = _assignments.FirstOrDefault(x => x.Id == assignmentId);
        if (assignment is null)
        {
            return Result.Failure(
                Error.NotFound("assignment.Missing", "This assignment could not be found")
            );
        }

        Result<AssignmentScore> assignmentScore = AssignmentScore.Create(
            assignment,
            studentId,
            score
        );
        assignment.AddAssignmentScore(assignmentScore.Value); // call the assignment instance to add the assignment not the assignment class
        return Result.Success();

        //Consider raising a domain event here.
    }

    public void UpdateAssignmentScore(StudentId studentId, AssignmentId assignmentId, int score)
    {
        Assignment? assignment =
            _assignments.FirstOrDefault(x => x.Id == assignmentId)
            ?? throw new InvalidOperationException(
                $"Assignment {assignmentId} not found for this course."
            );

        AssignmentScore? assignmentScore =
            assignment.AssignmentScores.FirstOrDefault(x => x.StudentId == studentId)
            ?? throw new InvalidOperationException(
                $"Score for student {studentId} not found for this assignment."
            );

        assignmentScore.UpdateScore(score);
        // raise a domain event here.
    }

    public void RemoveAssignmentScore(StudentId studentId, AssignmentId assignmentId)
    {
        Assignment? assignment =
            _assignments.FirstOrDefault(x => x.Id == assignmentId)
            ?? throw new InvalidOperationException(
                $"Assignment {assignmentId} not found for this course."
            );

        AssignmentScore? assignmentScore =
            assignment.AssignmentScores.FirstOrDefault(x => x.StudentId == studentId)
            ?? throw new InvalidOperationException(
                $"Score for student {studentId} not found for this assignment."
            );

        assignment.RemoveAssignmentScore(assignmentScore);
        // raise a domain event here.
    }

    // public record CourseOutlineRemovedEvent(CourseId CourseId, CourseOutlineId CourseOutlineId)
    //     : IDomainEvent;

    // public record AssignmentAddedEvent(CourseId CourseId, AssignmentId AssignmentId, string Title)
    //     : IDomainEvent;

    // public record StudentRegisteredEvent(CourseId CourseId, StudentId StudentId) : IDomainEvent;

    // public record TeacherAssignedEvent(CourseId CourseId, TeacherId TeacherId) : IDomainEvent;
}
