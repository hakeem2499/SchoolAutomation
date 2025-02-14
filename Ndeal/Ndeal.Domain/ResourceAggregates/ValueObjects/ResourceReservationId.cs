using Ndeal.SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.ValueObjects;

public class ResourceReservationId : ValueObject
{
    public Guid Value { get; }

    public ResourceReservationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ResourceReservationId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ResourceReservationId NewResourceReservationId()
    {
        return new ResourceReservationId(Guid.NewGuid());
    }

    public static ResourceReservationId FromGuid(Guid guid)
    {
        return new ResourceReservationId(guid);
    }

    public static implicit operator Guid(ResourceReservationId ResourceReservationId)
    {
        return ResourceReservationId.Value;
    }

    public static implicit operator ResourceReservationId(Guid guid)
    {
        return new ResourceReservationId(guid);
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
