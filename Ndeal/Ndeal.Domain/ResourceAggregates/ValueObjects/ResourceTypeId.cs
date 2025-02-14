using Ndeal.SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.ValueObjects;

public class ResourceTypeId : ValueObject
{
    public Guid Value { get; }

    public ResourceTypeId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ResourceTypeId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ResourceTypeId NewResourceTypeId()
    {
        return new ResourceTypeId(Guid.NewGuid());
    }

    public static ResourceTypeId FromGuid(Guid guid)
    {
        return new ResourceTypeId(guid);
    }

    public static implicit operator Guid(ResourceTypeId ResourceTypeId)
    {
        return ResourceTypeId.Value;
    }

    public static implicit operator ResourceTypeId(Guid guid)
    {
        return new ResourceTypeId(guid);
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
