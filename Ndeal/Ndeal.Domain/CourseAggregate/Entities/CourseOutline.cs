using Ndeal.Domain.CourseAggregate.ValueObjects;
using SharedKernel;

namespace Ndea.Domain.CourseAggregate.Entities;

public class CourseOutline : Entity<CourseOutlineId>
{
    public CourseOutline(
        CourseOutlineId courseOutlineId,
        string courseOutlineTitle,
        string courseOutlineDescription,
        string duration
    )
        : base(courseOutlineId)
    {
        CourseOutlineId = courseOutlineId;
        CourseOutlineTitle = courseOutlineTitle;
        CourseOutlineDescription = courseOutlineDescription;
        Duration = duration;
    }

    public CourseOutlineId CourseOutlineId { get; private set; }
    public string CourseOutlineTitle { get; private set; }
    public string CourseOutlineDescription { get; private set; }
    public string Duration { get; private set; }

    internal static CourseOutline Create(
        string courseOutlineTitle,
        string courseOutlineDescription,
        string duration
    )
    {
        var courseOutline = new CourseOutline(
            CourseOutlineId.NewCourseOutlineId(),
            courseOutlineTitle,
            courseOutlineDescription,
            duration
        );

        return courseOutline;
    }
}
