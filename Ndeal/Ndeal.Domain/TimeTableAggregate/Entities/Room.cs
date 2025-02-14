using Ndeal.Domain.TimeTableAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.TimeTableAggregate.Entities;

public class Room : Entity<RoomId>
{
    public Room(RoomId roomId, string name, int capacity)
        : base(roomId)
    {
        Name = name;
        Capacity = capacity;
    }

    public string Name { get; private set; }
    public int Capacity { get; private set; }

    internal static Room Create(string name, int capacity)
    {
        return new Room(RoomId.NewRoomId(), name, capacity);
    }

    internal void Update(string name, int capacity)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Capacity = capacity;
    }
}
