using Ndeal.SharedKernel;

namespace Ndeal.Domain.StudentAggregate.ValueObjects;

public class EnrollmentId : ValueObject
{
    public Guid Value { get; }

    public EnrollmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("EnrollmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static EnrollmentId NewEnrollmentId()
    {
        return new EnrollmentId(Guid.NewGuid());
    }

    public static EnrollmentId FromGuid(Guid guid)
    {
        return new EnrollmentId(guid);
    }

    public static implicit operator Guid(EnrollmentId EnrollmentId)
    {
        return EnrollmentId.Value;
    }

    public static implicit operator EnrollmentId(Guid guid)
    {
        return new EnrollmentId(guid);
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
