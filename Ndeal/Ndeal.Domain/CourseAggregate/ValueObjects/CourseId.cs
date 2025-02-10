using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate.ValueObjects;

public class CourseId : ValueObject
{
    public Guid Value { get; }

    public CourseId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("CourseId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static CourseId NewCourseId()
    {
        return new CourseId(Guid.NewGuid());
    }

    public static CourseId FromGuid(Guid guid)
    {
        return new CourseId(guid);
    }

    public static implicit operator Guid(CourseId CourseId)
    {
        return CourseId.Value;
    }

    public static implicit operator CourseId(Guid guid)
    {
        return new CourseId(guid);
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
