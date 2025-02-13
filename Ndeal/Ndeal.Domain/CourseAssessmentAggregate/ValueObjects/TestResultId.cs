using Ndeal.SharedKernel;

namespace Ndeal.Domain.AssessmentAggregate.ValueObjects;

public class TestResultId : ValueObject
{
    public Guid Value { get; }

    public TestResultId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TestResultId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static TestResultId NewTestResultId()
    {
        return new TestResultId(Guid.NewGuid());
    }

    public static TestResultId FromGuid(Guid guid)
    {
        return new TestResultId(guid);
    }

    public static implicit operator Guid(TestResultId TestResultId)
    {
        return TestResultId.Value;
    }

    public static implicit operator TestResultId(Guid guid)
    {
        return new TestResultId(guid);
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
