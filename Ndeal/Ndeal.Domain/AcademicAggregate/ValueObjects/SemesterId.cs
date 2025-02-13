using Ndeal.SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.ValueObjects;

public class SemesterId : ValueObject
{
    private static int _lastId = 0; // Static field to keep track of the last used ID

    public int Value { get; }

    public SemesterId(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("SemesterId must be a positive integer", nameof(value));
        }

        Value = value;
    }

    public static SemesterId NewSemesterId()
    {
        _lastId++; // Increment the last used ID
        return new SemesterId(_lastId);
    }

    public static implicit operator int(SemesterId SemesterId)
    {
        return SemesterId.Value;
    }

    public static implicit operator SemesterId(int value)
    {
        return new SemesterId(value);
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
