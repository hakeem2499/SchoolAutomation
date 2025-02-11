using Ndeal.SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.ValueObjects;

public class ResourceId : ValueObject
{
    public Guid Value { get; }

    public ResourceId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ResourceId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ResourceId NewResourceId()
    {
        return new ResourceId(Guid.NewGuid());
    }

    public static ResourceId FromGuid(Guid guid)
    {
        return new ResourceId(guid);
    }

    public static implicit operator Guid(ResourceId ResourceId)
    {
        return ResourceId.Value;
    }

    public static implicit operator ResourceId(Guid guid)
    {
        return new ResourceId(guid);
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
