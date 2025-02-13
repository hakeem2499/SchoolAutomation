using Ndeal.SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.ValueObjects;

public class AcademicSessionId : ValueObject
{
    private static int _lastId = 0; // Static field to keep track of the last used ID

    public int Value { get; }

    public AcademicSessionId(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException(
                "AcademicSessionId must be a positive integer",
                nameof(value)
            );
        }

        Value = value;
    }

    public static AcademicSessionId NewAcademicSessionId()
    {
        _lastId++; // Increment the last used ID
        return new AcademicSessionId(_lastId);
    }

    public static implicit operator int(AcademicSessionId AcademicSessionId)
    {
        return AcademicSessionId.Value;
    }

    public static implicit operator AcademicSessionId(int value)
    {
        return new AcademicSessionId(value);
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
