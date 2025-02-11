using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate.ValueObjects;

public class AssignmentScoreId : ValueObject
{
    public Guid Value { get; }

    public AssignmentScoreId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("AssignmentScoreId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static AssignmentScoreId NewAssignmentScoreId()
    {
        return new AssignmentScoreId(Guid.NewGuid());
    }

    public static AssignmentScoreId FromGuid(Guid guid)
    {
        return new AssignmentScoreId(guid);
    }

    public static implicit operator Guid(AssignmentScoreId AssignmentScoreId)
    {
        return AssignmentScoreId.Value;
    }

    public static implicit operator AssignmentScoreId(Guid guid)
    {
        return new AssignmentScoreId(guid);
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
