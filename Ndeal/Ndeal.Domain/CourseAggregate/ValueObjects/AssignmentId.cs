using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate.ValueObjects;

public class AssignmentId : ValueObject
{
    public Guid Value { get; }

    public AssignmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("AssignmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static AssignmentId NewAssignmentId()
    {
        return new AssignmentId(Guid.NewGuid());
    }

    public static AssignmentId FromGuid(Guid guid)
    {
        return new AssignmentId(guid);
    }

    public static implicit operator Guid(AssignmentId AssignmentId)
    {
        return AssignmentId.Value;
    }

    public static implicit operator AssignmentId(Guid guid)
    {
        return new AssignmentId(guid);
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
