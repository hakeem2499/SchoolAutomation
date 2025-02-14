using Ndeal.Domain.ResourceAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.Entities;

public class ResourceMaintenance : Entity<ResourceMaintenanceId>
{
    public ResourceMaintenance(
        ResourceMaintenanceId maintenanceId,
        ResourceId resourceId,
        string reason,
        DateTime maintenanceDate
    )
        : base(maintenanceId)
    {
        ResourceId = resourceId;
        Reason = reason;
        MaintenanceDate = maintenanceDate;
    }

    public ResourceId ResourceId { get; private set; }
    public string Reason { get; private set; }
    public DateTime MaintenanceDate { get; private set; }

    public static ResourceMaintenance Create(
        ResourceId resourceId,
        string reason,
        DateTime maintenanceDate
    )
    {
        return new ResourceMaintenance(
            ResourceMaintenanceId.NewResourceMaintenanceId(),
            resourceId,
            reason,
            maintenanceDate
        );
    }
}
