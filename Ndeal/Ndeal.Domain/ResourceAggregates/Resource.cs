using Ndeal.Domain.ResourceAggregate.Entities;
using Ndeal.Domain.ResourceAggregate.Enums;
using Ndeal.Domain.ResourceAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.ResourceAggregate;

public class Resource : Entity<ResourceId>
{
    private readonly List<ResourceAssignment> _assignments = new();
    private readonly List<ResourceReservation> _reservations = new();
    private readonly List<ResourceMaintenance> _maintenanceRecords = new();

    public Resource(
        ResourceId resourceId,
        ResourceTypeId resourceTypeId,
        string name,
        string description,
        ResourceStatus status
    )
        : base(resourceId)
    {
        ResourceTypeId = resourceTypeId;
        Name = name;
        Description = description;
        Status = status;
    }

    public ResourceTypeId ResourceTypeId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ResourceStatus Status { get; private set; }

    public IReadOnlyCollection<ResourceAssignment> Assignments => _assignments.AsReadOnly();
    public IReadOnlyCollection<ResourceReservation> Reservations => _reservations.AsReadOnly();
    public IReadOnlyCollection<ResourceMaintenance> MaintenanceRecords =>
        _maintenanceRecords.AsReadOnly();

    public static Resource Create(ResourceTypeId resourceTypeId, string name, string description)
    {
        return new Resource(
            ResourceId.NewResourceId(),
            resourceTypeId,
            name,
            description,
            ResourceStatus.Available
        );
    }

    public Result AssignResource(StudentId studentId, DateTime startDate, DateTime endDate)
    {
        if (Status != ResourceStatus.Available)
        {
            return Result.Failure<Resource>(ResourceErrors.ResourceUnavailable(this));
        }

        Result<ResourceAssignment> assignment = ResourceAssignment.Create(
            Id,
            studentId,
            startDate,
            endDate
        );
        _assignments.Add(assignment.Value);
        Status = ResourceStatus.Assigned;
        return Result.Success();
    }

    public void ReserveResource(TeacherId teacherId, DateTime startDate, DateTime endDate)
    {
        if (Status != ResourceStatus.Available)
        {
            throw new InvalidOperationException("Resource is not available for reservation.");
        }

        var reservation = ResourceReservation.Create(Id, teacherId, startDate, endDate);
        _reservations.Add(reservation);
        Status = ResourceStatus.Reserved;
    }

    public void MarkAsUnderMaintenance(string reason, DateTime maintenanceDate)
    {
        var maintenance = ResourceMaintenance.Create(Id, reason, maintenanceDate);
        _maintenanceRecords.Add(maintenance);
        Status = ResourceStatus.UnderMaintenance;
    }

    public void CompleteMaintenance()
    {
        if (Status != ResourceStatus.UnderMaintenance)
        {
            throw new InvalidOperationException("Resource is not under maintenance.");
        }

        Status = ResourceStatus.Available;
    }

    public void UpdateResource(string name, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}
