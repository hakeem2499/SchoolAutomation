using Ndeal.SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.ValueObjects;

public class ResourceAssignmentId : ValueObject
{
    public Guid Value { get; }

    public ResourceAssignmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ResourceAssignmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ResourceAssignmentId NewResourceAssignmentId()
    {
        return new ResourceAssignmentId(Guid.NewGuid());
    }

    public static ResourceAssignmentId FromGuid(Guid guid)
    {
        return new ResourceAssignmentId(guid);
    }

    public static implicit operator Guid(ResourceAssignmentId ResourceAssignmentId)
    {
        return ResourceAssignmentId.Value;
    }

    public static implicit operator ResourceAssignmentId(Guid guid)
    {
        return new ResourceAssignmentId(guid);
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
