using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate;

public class Course : AggregateRoot<CourseId>
{
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

    
}
