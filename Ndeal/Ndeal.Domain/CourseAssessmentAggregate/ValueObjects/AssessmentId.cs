using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;

public class AssessmentId : ValueObject
{
    public Guid Value { get; }

    public AssessmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("AssessmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static AssessmentId NewAssessmentId()
    {
        return new AssessmentId(Guid.NewGuid());
    }

    public static AssessmentId FromGuid(Guid guid)
    {
        return new AssessmentId(guid);
    }

    public static implicit operator Guid(AssessmentId AssessmentId)
    {
        return AssessmentId.Value;
    }

    public static implicit operator AssessmentId(Guid guid)
    {
        return new AssessmentId(guid);
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
