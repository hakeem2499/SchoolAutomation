using Ndea.Domain.CourseAggregate.Entities;
using Ndeal.Domain.AssessmentAggregate.ValueObjects;
using Ndeal.Domain.AttendanceAggregate.ValueObjects;
using Ndeal.Domain.CourseAggregate.Entities;
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate;

public class Course : AggregateRoot<CourseId>
{
    private readonly List<CourseOutline> _courseOutlines = new();
    private readonly List<Assignment> _assignments = new();
    private readonly List<StudentRegistration> _studentRegistrations = new();
    private readonly List<TeacherAssignment> _teacherAssignments = new();
    private readonly List<AssessmentId> _assessmentIds = new();
    private readonly List<AttendanceId> _attendanceIds = new();

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
    public IReadOnlyList<AssessmentId> AssessmentIds => _assessmentIds.AsReadOnly();
    public IReadOnlyList<AttendanceId> AttendanceIds => _attendanceIds.AsReadOnly();

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

    // This is a method for adding a course outline to a course
    public void AddCourseOutline(
        string CourseOutlineTitle,
        string CourseOutlineDescription,
        string duration
    )
    {
        var courseOutline = CourseOutline.Create(
            CourseOutlineTitle,
            CourseOutlineDescription,
            duration
        );
        _courseOutlines.Add(courseOutline);
    }

    // This is a method for removing a course outline from a course
    public void RemoveCourseOutline(CourseOutlineId courseOutlineId)
    {
        CourseOutline? courseOutline = _courseOutlines.FirstOrDefault(co =>
            co.Id == courseOutlineId
        );
        if (courseOutline is not null)
        {
            _courseOutlines.Remove(courseOutline);
        }
    }

    // This is a method for adding an assignment to a course
    public void AddAssignment(
        string title,
        string description,
        DateTime dueDate,
        TeacherId teacherId,
        int maxScore
    )
    {
        var assignment = Assignment.Create(title, description, dueDate, this, teacherId, maxScore);
        _assignments.Add(assignment);
    }

    // This is a method for removing an assignment from a course
    public void RemoveAssignment(AssignmentId assignmentId)
    {
        Assignment? assignment = _assignments.FirstOrDefault(a => a.Id == assignmentId);
        if (assignment is not null)
        {
            _assignments.Remove(assignment);
        }
    }

    // This is a method for registering a student to a course
    public void RegisterStudent(StudentId studentId, DepartmentId departmentId)
    {
        var studentRegistration = StudentRegistration.Create(studentId, this, departmentId);
        _studentRegistrations.Add(studentRegistration);
    }

    // This is a method for removing a student from a course
    public void RemoveStudent(StudentId studentId)
    {
        StudentRegistration? studentRegistration = _studentRegistrations.FirstOrDefault(sr =>
            sr.StudentId == studentId
        );
        if (studentRegistration is not null)
        {
            _studentRegistrations.Remove(studentRegistration);
        }
    }

    // This is a method for assigning a teacher to a course

    public void AssignTeacher(TeacherId teacherId)
    {
        var teacherAssignment = TeacherAssignment.Create(teacherId, this);
        _teacherAssignments.Add(teacherAssignment);
    }

    // This is a method for removing a teacher from a course
    public void RemoveTeacher(TeacherId teacherId)
    {
        TeacherAssignment? teacherAssignment = _teacherAssignments.FirstOrDefault(ta =>
            ta.TeacherId == teacherId
        );
        if (teacherAssignment is not null)
        {
            _teacherAssignments.Remove(teacherAssignment);
        }
    }

    // This is a method for adding an assessment to a course
    public void AddAssessment(AssessmentId assessmentId)
    {
        _assessmentIds.Add(assessmentId);
    }

    // This is a method for adding an attendance to a course
    public void AddAttendance(AttendanceId attendanceId)
    {
        _attendanceIds.Add(attendanceId);
    }

    // This is a method for removing an attendance from a course
}
