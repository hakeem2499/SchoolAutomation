using Ndeal.SharedKernel;

namespace Ndeal.Domain.StudentAggregate.ValueObjects;

public class StudentId : ValueObject
{
    public Guid Value { get; }

    public StudentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("StudentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static StudentId NewStudentId()
    {
        return new StudentId(Guid.NewGuid());
    }

    public static StudentId FromGuid(Guid guid)
    {
        return new StudentId(guid);
    }

    public static implicit operator Guid(StudentId studentId)
    {
        return studentId.Value;
    }

    public static implicit operator StudentId(Guid guid)
    {
        return new StudentId(guid);
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
