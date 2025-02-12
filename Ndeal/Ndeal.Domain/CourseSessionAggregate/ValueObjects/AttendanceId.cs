using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseSessionAggregate.ValueObjects;

public class AttendanceId : ValueObject
{
    public Guid Value { get; }

    public AttendanceId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("AttendanceId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static AttendanceId NewAttendanceId()
    {
        return new AttendanceId(Guid.NewGuid());
    }

    public static AttendanceId FromGuid(Guid guid)
    {
        return new AttendanceId(guid);
    }

    public static implicit operator Guid(AttendanceId AttendanceId)
    {
        return AttendanceId.Value;
    }

    public static implicit operator AttendanceId(Guid guid)
    {
        return new AttendanceId(guid);
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
