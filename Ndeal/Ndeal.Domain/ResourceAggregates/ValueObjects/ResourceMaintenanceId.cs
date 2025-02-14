using Ndeal.SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.ValueObjects;

public class ResourceMaintenanceId : ValueObject
{
    public Guid Value { get; }

    public ResourceMaintenanceId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ResourceMaintenanceId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ResourceMaintenanceId NewResourceMaintenanceId()
    {
        return new ResourceMaintenanceId(Guid.NewGuid());
    }

    public static ResourceMaintenanceId FromGuid(Guid guid)
    {
        return new ResourceMaintenanceId(guid);
    }

    public static implicit operator Guid(ResourceMaintenanceId ResourceMaintenanceId)
    {
        return ResourceMaintenanceId.Value;
    }

    public static implicit operator ResourceMaintenanceId(Guid guid)
    {
        return new ResourceMaintenanceId(guid);
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
