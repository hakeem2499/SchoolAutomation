using Ndeal.SharedKernel;

namespace Ndeal.Domain.TimeTableAggregate.ValueObjects;

public class TimetableEntryId : ValueObject
{
    public Guid Value { get; }

    public TimetableEntryId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TimetableEntryId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static TimetableEntryId NewTimetableEntryId()
    {
        return new TimetableEntryId(Guid.NewGuid());
    }

    public static TimetableEntryId FromGuid(Guid guid)
    {
        return new TimetableEntryId(guid);
    }

    public static implicit operator Guid(TimetableEntryId TimetableEntryId)
    {
        return TimetableEntryId.Value;
    }

    public static implicit operator TimetableEntryId(Guid guid)
    {
        return new TimetableEntryId(guid);
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
