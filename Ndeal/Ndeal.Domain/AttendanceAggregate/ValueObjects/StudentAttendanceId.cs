using Ndeal.SharedKernel;

namespace Ndeal.Domain.AttendanceAggregate.ValueObjects;

public class StudentAttendanceId : ValueObject
{
    public Guid Value { get; }

    public StudentAttendanceId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("StudentAttendanceId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static StudentAttendanceId NewStudentAttendanceId()
    {
        return new StudentAttendanceId(Guid.NewGuid());
    }

    public static StudentAttendanceId FromGuid(Guid guid)
    {
        return new StudentAttendanceId(guid);
    }

    public static implicit operator Guid(StudentAttendanceId studentAttendanceId)
    {
        return studentAttendanceId.Value;
    }

    public static implicit operator StudentAttendanceId(Guid guid)
    {
        return new StudentAttendanceId(guid);
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
