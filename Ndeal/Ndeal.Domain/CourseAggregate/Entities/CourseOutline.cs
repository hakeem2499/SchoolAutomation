using Ndeal.Domain.CourseAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate.Entities;

public class CourseOutline : Entity<CourseOutlineId>
{
    public CourseOutline(
        CourseOutlineId courseOutlineId,
        CourseId courseId,
        string courseOutlineTitle,
        string courseOutlineDescription,
        string duration
    )
        : base(courseOutlineId)
    {
        CourseId = courseId;
        CourseOutlineTitle = courseOutlineTitle;
        CourseOutlineDescription = courseOutlineDescription;
        Duration = duration;
    }

    public CourseId CourseId { get; private set; }
    public string CourseOutlineTitle { get; private set; }
    public string CourseOutlineDescription { get; private set; }
    public string Duration { get; private set; }

    internal static CourseOutline Create(
        CourseId courseId,
        string courseOutlineTitle,
        string courseOutlineDescription,
        string duration
    )
    {
        var courseOutline = new CourseOutline(
            CourseOutlineId.NewCourseOutlineId(),
            courseId,
            courseOutlineTitle,
            courseOutlineDescription,
            duration
        );

        return courseOutline;
    }
}
