using Ndeal.SharedKernel;

namespace Ndeal.Domain.TimeTableAggregate.ValueObjects;

public class RoomId : ValueObject
{
    public Guid Value { get; }

    public RoomId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("RoomId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static RoomId NewRoomId()
    {
        return new RoomId(Guid.NewGuid());
    }

    public static RoomId FromGuid(Guid guid)
    {
        return new RoomId(guid);
    }

    public static implicit operator Guid(RoomId RoomId)
    {
        return RoomId.Value;
    }

    public static implicit operator RoomId(Guid guid)
    {
        return new RoomId(guid);
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
