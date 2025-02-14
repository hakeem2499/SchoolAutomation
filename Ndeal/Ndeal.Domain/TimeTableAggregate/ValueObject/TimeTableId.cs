using Ndeal.SharedKernel;

namespace Ndeal.Domain.TimeTableAggregate.ValueObjects;

public class TimeTableId : ValueObject
{
    private static int _lastId = 0; // Static field to keep track of the last used ID

    public int Value { get; }

    public TimeTableId(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("TimeTableId must be a positive integer", nameof(value));
        }

        Value = value;
    }

    public static TimeTableId NewTimeTableId()
    {
        _lastId++; // Increment the last used ID
        return new TimeTableId(_lastId);
    }

    public static implicit operator int(TimeTableId TimeTableId)
    {
        return TimeTableId.Value;
    }

    public static implicit operator TimeTableId(int value)
    {
        return new TimeTableId(value);
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
