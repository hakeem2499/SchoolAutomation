using Ndeal.SharedKernel;

namespace Ndeal.Domain.UserAggregate.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static UserId NewUserId()
    {
        return new UserId(Guid.NewGuid());
    }

    public static UserId FromGuid(Guid guid)
    {
        return new UserId(guid);
    }

    public static implicit operator Guid(UserId UserId)
    {
        return UserId.Value;
    }

    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
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
