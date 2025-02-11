using System;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate.Entities;

public class ScheduleEvent : Entity<ScheduleEventId>
{
    public ScheduleEvent(
        ScheduleEventId id,
        DepartmentId departmentId,
        DateTime date,
        string description
    )
        : base(id)
    {
        DepartmentId = departmentId;
        Date = date;
        Description = description;
    }

    public DepartmentId DepartmentId { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }

    internal static ScheduleEvent Create(
        DepartmentId departmentId,
        DateTime date,
        string description
    )
    {
        var scheduleEvent = new ScheduleEvent(
            ScheduleEventId.NewScheduleEventId(),
            departmentId,
            date,
            description
        );

        return scheduleEvent;
    }
}
