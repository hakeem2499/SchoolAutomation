using Ndeal.Domain.ResourceAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.Entities;

public sealed class ResourceType : Entity<ResourceTypeId>
{
    public ResourceType(ResourceTypeId resourceTypeId, string name, string description)
        : base(resourceTypeId)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public static ResourceType Create(string name, string description)
    {
        return new ResourceType(ResourceTypeId.NewResourceTypeId(), name, description);
    }

    public void Update(string name, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}
