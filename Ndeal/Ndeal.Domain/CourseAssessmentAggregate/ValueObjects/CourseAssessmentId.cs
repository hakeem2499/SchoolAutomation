using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;

public class CourseAssessmentId : ValueObject
{
    public Guid Value { get; }

    public CourseAssessmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("CourseAssessmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static CourseAssessmentId NewCourseAssessmentId()
    {
        return new CourseAssessmentId(Guid.NewGuid());
    }

    public static CourseAssessmentId FromGuid(Guid guid)
    {
        return new CourseAssessmentId(guid);
    }

    public static implicit operator Guid(CourseAssessmentId CourseAssessmentId)
    {
        return CourseAssessmentId.Value;
    }

    public static implicit operator CourseAssessmentId(Guid guid)
    {
        return new CourseAssessmentId(guid);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
