using Ndeal.Domain.ResourceAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.Entities;

public class ResourceReservation : Entity<ResourceReservationId>
{
    public ResourceReservation(
        ResourceReservationId reservationId,
        ResourceId resourceId,
        TeacherId teacherId,
        DateTime startDate,
        DateTime endDate
    )
        : base(reservationId)
    {
        ResourceId = resourceId;
        TeacherId = teacherId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public ResourceId ResourceId { get; private set; }
    public TeacherId TeacherId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    internal static ResourceReservation Create(
        ResourceId resourceId,
        TeacherId teacherId,
        DateTime startDate,
        DateTime endDate
    )
    {
        return new ResourceReservation(
            ResourceReservationId.NewResourceReservationId(),
            resourceId,
            teacherId,
            startDate,
            endDate
        );
    }

    internal void Update(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}
