using Ndeal.SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate.ValueObjects;

public class StudentEnrollmentId : ValueObject
{
    public Guid Value { get; }

    public StudentEnrollmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("StudentEnrollmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static StudentEnrollmentId NewStudentEnrollmentId()
    {
        return new StudentEnrollmentId(Guid.NewGuid());
    }

    public static StudentEnrollmentId FromGuid(Guid guid)
    {
        return new StudentEnrollmentId(guid);
    }

    public static implicit operator Guid(StudentEnrollmentId StudentEnrollmentId)
    {
        return StudentEnrollmentId.Value;
    }

    public static implicit operator StudentEnrollmentId(Guid guid)
    {
        return new StudentEnrollmentId(guid);
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
