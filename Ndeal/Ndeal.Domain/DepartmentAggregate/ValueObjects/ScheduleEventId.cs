using Ndeal.SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate.ValueObjects;

public class ScheduleEventId : ValueObject
{
    public Guid Value { get; }

    public ScheduleEventId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ScheduleEventId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ScheduleEventId NewScheduleEventId()
    {
        return new ScheduleEventId(Guid.NewGuid());
    }

    public static ScheduleEventId FromGuid(Guid guid)
    {
        return new ScheduleEventId(guid);
    }

    public static implicit operator Guid(ScheduleEventId ScheduleEventId)
    {
        return ScheduleEventId.Value;
    }

    public static implicit operator ScheduleEventId(Guid guid)
    {
        return new ScheduleEventId(guid);
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
