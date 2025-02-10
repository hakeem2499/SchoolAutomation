using Ndeal.SharedKernel;

namespace Ndeal.Domain.StudentAggregate.ValueObjects;

public class GuardianId : ValueObject
{
    public Guid Value { get; }

    public GuardianId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("GuardianId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static GuardianId NewGuardianId()
    {
        return new GuardianId(Guid.NewGuid());
    }

    public static GuardianId FromGuid(Guid guid)
    {
        return new GuardianId(guid);
    }

    public static implicit operator Guid(GuardianId GuardianId)
    {
        return GuardianId.Value;
    }

    public static implicit operator GuardianId(Guid guid)
    {
        return new GuardianId(guid);
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
