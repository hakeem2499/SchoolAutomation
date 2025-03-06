using Ndeal.Domain.ResourceAggregate.Entities;
using Ndeal.Domain.ResourceAggregate.Enums;
using Ndeal.Domain.ResourceAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.ResourceAggregate;

public class Resource : Entity<ResourceId>
{
    private readonly List<ResourceAssignment> _assignments = new();
    private readonly List<ResourceReservation> _reservations = new();
    private readonly List<ResourceMaintenance> _maintenanceRecords = new();

    // Private constructor for controlled creation
    private Resource(
        ResourceId resourceId,
        ResourceTypeId resourceTypeId,
        string name,
        string description,
        ResourceStatus status,
        bool allowsMultipleUsers
    )
        : base(resourceId)
    {
        ResourceTypeId = resourceTypeId;
        Name = name;
        Description = description;
        Status = status;
        AllowsMultipleUsers = allowsMultipleUsers;
    }

    // Properties with private setters for encapsulation
    public ResourceTypeId ResourceTypeId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ResourceStatus Status { get; private set; }
    public bool AllowsMultipleUsers { get; private set; } // Determines if resource supports multiple assignments
    public IReadOnlyCollection<ResourceAssignment> Assignments => _assignments.AsReadOnly();
    public IReadOnlyCollection<ResourceReservation> Reservations => _reservations.AsReadOnly();
    public IReadOnlyCollection<ResourceMaintenance> MaintenanceRecords =>
        _maintenanceRecords.AsReadOnly();

    // Factory method with validation
    public static Result<Resource> Create(
        ResourceTypeId resourceTypeId,
        string name,
        string description,
        bool allowsMultipleUsers
    )
    {
        if (resourceTypeId is null)
        {
            return Result.Failure<Resource>(
                Error.Validation("ResourceTypeId.Required", "Resource type ID is required.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Resource>(Error.Validation("Name.Required", "Name is required."));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Resource>(
                Error.Validation("Description.Required", "Description is required.")
            );
        }

        var resource = new Resource(
            ResourceId.NewResourceId(),
            resourceTypeId,
            name,
            description,
            ResourceStatus.Available,
            allowsMultipleUsers
        );
        // Raise(new ResourceCreatedEvent(resource.Id, resourceTypeId, name, allowsMultipleUsers));
        return Result.Success(resource);
    }

    // Update method with validation
    public Result UpdateResource(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(Error.Validation("Name.Required", "Name is required."));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure(
                Error.Validation("Description.Required", "Description is required.")
            );
        }

        Name = name;
        Description = description;
        // Raise(new ResourceUpdatedEvent(Id, Name, Description));
        return Result.Success();
    }

    public Result AssignResource(StudentId studentId, DateTime startDate, DateTime endDate)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (startDate < DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation("StartDate.Invalid", "Start date cannot be in the past.")
            );
        }

        if (endDate <= startDate)
        {
            return Result.Failure(
                Error.Validation("EndDate.Invalid", "End date must be after start date.")
            );
        }

        if (Status == ResourceStatus.UnderMaintenance)
        {
            return Result.Failure(
                Error.Conflict("Resource.Unavailable", $"Resource {Id} is under maintenance.")
            );
        }

        if (
            !AllowsMultipleUsers
            && (Status == ResourceStatus.Assigned || Status == ResourceStatus.Reserved)
        )
        {
            return Result.Failure(
                Error.Conflict(
                    "Resource.Unavailable",
                    $"Single-user resource {Id} is already assigned or reserved."
                )
            );
        }

        if (
            !AllowsMultipleUsers
            && _assignments.Any(a => a.StartDate < endDate && a.EndDate > startDate)
        )
        {
            return Result.Failure(
                Error.Conflict(
                    "Resource.AlreadyAssigned",
                    $"Single-user resource {Id} is already assigned during this period."
                )
            );
        }

        if (AllowsMultipleUsers && Status == ResourceStatus.Reserved)
        {
            return Result.Failure(
                Error.Conflict(
                    "Resource.Reserved",
                    $"Multi-user resource {Id} cannot be assigned while reserved."
                )
            );
        }

        Result<ResourceAssignment> assignmentResult = ResourceAssignment.Create(
            Id,
            studentId,
            startDate,
            endDate
        );
        if (assignmentResult.IsFailure)
        {
            return assignmentResult;
        }

        _assignments.Add(assignmentResult.Value);
        Status = ResourceStatus.Assigned; // Always Assigned when assigned, regardless of multi-user
        // Raise(new ResourceAssignedEvent(Id, studentId, startDate, endDate));
        return Result.Success();
    }

    public Result RemoveAssignment(StudentId studentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        ResourceAssignment? assignment = _assignments.FirstOrDefault(a => a.StudentId == studentId);
        if (assignment == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Assignment.NotFound",
                    $"Assignment for student {studentId} not found."
                )
            );
        }

        _assignments.Remove(assignment);
        if (_assignments.Count == 0 && _reservations.Count == 0) // Reset to Available if no assignments or reservations
        {
            Status = ResourceStatus.Available;
        }
        // Raise(new ResourceAssignmentRemovedEvent(Id, studentId));
        return Result.Success();
    }

    public Result ReserveResource(TeacherId teacherId, DateTime startDate, DateTime endDate)
    {
        if (teacherId is null)
        {
            return Result.Failure(
                Error.Validation("TeacherId.Required", "Teacher ID is required.")
            );
        }

        if (startDate < DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation("StartDate.Invalid", "Start date cannot be in the past.")
            );
        }

        if (endDate <= startDate)
        {
            return Result.Failure(
                Error.Validation("EndDate.Invalid", "End date must be after start date.")
            );
        }

        if (AllowsMultipleUsers)
        {
            return Result.Failure(
                Error.Validation(
                    "Resource.MultiUserNotReservable",
                    $"Multi-user resource {Id} cannot be reserved."
                )
            );
        }

        if (Status != ResourceStatus.Available)
        {
            return Result.Failure(
                Error.Conflict(
                    "Resource.Unavailable",
                    $"Single-user resource {Id} is not available (current status: {Status})."
                )
            );
        }

        if (_reservations.Any(r => r.StartDate < endDate && r.EndDate > startDate))
        {
            return Result.Failure(
                Error.Conflict(
                    "Resource.AlreadyReserved",
                    $"Single-user resource {Id} is already reserved during this period."
                )
            );
        }

        Result<ResourceReservation> reservationResult = ResourceReservation.Create(
            Id,
            teacherId,
            startDate,
            endDate
        );
        if (reservationResult.IsFailure)
        {
            return reservationResult;
        }

        _reservations.Add(reservationResult.Value);
        Status = ResourceStatus.Reserved; // Single-user resources become Reserved
        // Raise(new ResourceReservedEvent(Id, teacherId, startDate, endDate));
        return Result.Success();
    }

    public Result RemoveReservation(TeacherId teacherId)
    {
        if (teacherId is null)
        {
            return Result.Failure(
                Error.Validation("TeacherId.Required", "Teacher ID is required.")
            );
        }

        ResourceReservation? reservation = _reservations.FirstOrDefault(r =>
            r.TeacherId == teacherId
        );
        if (reservation == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Reservation.NotFound",
                    $"Reservation for teacher {teacherId} not found."
                )
            );
        }

        _reservations.Remove(reservation);
        if (_assignments.Count == 0 && _reservations.Count == 0) // Reset to Available if no assignments or reservations
        {
            Status = ResourceStatus.Available;
        }
        // Raise(new ResourceReservationRemovedEvent(Id, teacherId));
        return Result.Success();
    }

    public Result MarkAsUnderMaintenance(string reason, DateTime maintenanceDate)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            return Result.Failure(
                Error.Validation("Reason.Required", "Maintenance reason is required.")
            );
        }

        if (maintenanceDate < DateTime.UtcNow)
        {
            return Result.Failure(
                Error.Validation(
                    "MaintenanceDate.Invalid",
                    "Maintenance date cannot be in the past."
                )
            );
        }

        if (Status == ResourceStatus.UnderMaintenance)
        {
            return Result.Failure(
                Error.Conflict(
                    "Resource.AlreadyUnderMaintenance",
                    $"Resource {Id} is already under maintenance."
                )
            );
        }

        if (_assignments.Any() || _reservations.Any())
        {
            return Result.Failure(
                Error.Conflict(
                    "Resource.InUse",
                    $"Resource {Id} cannot be maintained while assigned or reserved."
                )
            );
        }

        Result<ResourceMaintenance> maintenanceResult = ResourceMaintenance.Create(Id, reason, maintenanceDate);
        if (maintenanceResult.IsFailure)
        {
            return maintenanceResult;
        }

        _maintenanceRecords.Add(maintenanceResult.Value);
        Status = ResourceStatus.UnderMaintenance;
        // Raise(new ResourceMaintenanceStartedEvent(Id, reason, maintenanceDate));
        return Result.Success();
    }

    public Result CompleteMaintenance()
    {
        if (Status != ResourceStatus.UnderMaintenance)
        {
            return Result.Failure(
                Error.Validation(
                    "Resource.NotUnderMaintenance",
                    $"Resource {Id} is not under maintenance (current status: {Status})."
                )
            );
        }

        Status = ResourceStatus.Available;
        // Raise(new ResourceMaintenanceCompletedEvent(Id));
        return Result.Success();
    }
}
