using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseSessionAggregate.ValueObjects;

public class CourseSessionId : ValueObject
{
    public Guid Value { get; }

    public CourseSessionId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("CourseSessionId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static CourseSessionId NewCourseSessionId()
    {
        return new CourseSessionId(Guid.NewGuid());
    }

    public static CourseSessionId FromGuid(Guid guid)
    {
        return new CourseSessionId(guid);
    }

    public static implicit operator Guid(CourseSessionId CourseSessionId)
    {
        return CourseSessionId.Value;
    }

    public static implicit operator CourseSessionId(Guid guid)
    {
        return new CourseSessionId(guid);
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
