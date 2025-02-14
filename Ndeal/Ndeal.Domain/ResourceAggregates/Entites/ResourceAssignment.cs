using Ndeal.Domain.ResourceAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.Entities;

public class ResourceAssignment : Entity<ResourceAssignmentId>
{
    public ResourceAssignment(
        ResourceAssignmentId assignmentId,
        ResourceId resourceId,
        StudentId studentId,
        DateTime startDate,
        DateTime endDate
    )
        : base(assignmentId)
    {
        ResourceId = resourceId;
        StudentId = studentId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public ResourceId ResourceId { get; private set; }
    public StudentId StudentId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public static ResourceAssignment Create(
        ResourceId resourceId,
        StudentId studentId,
        DateTime startDate,
        DateTime endDate
    )
    {
        return new ResourceAssignment(
            ResourceAssignmentId.NewResourceAssignmentId(),
            resourceId,
            studentId,
            startDate,
            endDate
        );
    }
}
