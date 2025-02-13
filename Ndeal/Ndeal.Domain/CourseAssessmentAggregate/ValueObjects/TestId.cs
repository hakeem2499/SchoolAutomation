using Ndeal.SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.ValueObjects;

public class TestId : ValueObject
{
    private static int _lastId = 0; // Static field to keep track of the last used ID

    public int Value { get; }

    public TestId(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("TestId must be a positive integer", nameof(value));
        }

        Value = value;
    }

    public static TestId NewTestId()
    {
        _lastId++; // Increment the last used ID
        return new TestId(_lastId);
    }

    public static implicit operator int(TestId TestId)
    {
        return TestId.Value;
    }

    public static implicit operator TestId(int value)
    {
        return new TestId(value);
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
