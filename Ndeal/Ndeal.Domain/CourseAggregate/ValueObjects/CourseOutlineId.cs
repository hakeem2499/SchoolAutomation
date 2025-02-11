using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate.ValueObjects;

public class CourseOutlineId : ValueObject
{
    public Guid Value { get; }

    public CourseOutlineId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("CourseOutlineId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static CourseOutlineId NewCourseOutlineId()
    {
        return new CourseOutlineId(Guid.NewGuid());
    }

    public static CourseOutlineId FromGuid(Guid guid)
    {
        return new CourseOutlineId(guid);
    }

    public static implicit operator Guid(CourseOutlineId CourseOutlineId)
    {
        return CourseOutlineId.Value;
    }

    public static implicit operator CourseOutlineId(Guid guid)
    {
        return new CourseOutlineId(guid);
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
