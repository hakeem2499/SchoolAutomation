using Ndeal.SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.ValueObjects;

public class StudentPaymentId : ValueObject
{
    public Guid Value { get; }

    public StudentPaymentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("StudentPaymentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static StudentPaymentId NewStudentPaymentId()
    {
        return new StudentPaymentId(Guid.NewGuid());
    }

    public static StudentPaymentId FromGuid(Guid guid)
    {
        return new StudentPaymentId(guid);
    }

    public static implicit operator Guid(StudentPaymentId StudentPaymentId)
    {
        return StudentPaymentId.Value;
    }

    public static implicit operator StudentPaymentId(Guid guid)
    {
        return new StudentPaymentId(guid);
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
