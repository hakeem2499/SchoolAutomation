using Ndeal.SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.ValueObjects;

public class PaymentId : ValueObject
{
    public Guid Value { get; }

    public PaymentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("PaymentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static PaymentId NewPaymentId()
    {
        return new PaymentId(Guid.NewGuid());
    }

    public static PaymentId FromGuid(Guid guid)
    {
        return new PaymentId(guid);
    }

    public static implicit operator Guid(PaymentId PaymentId)
    {
        return PaymentId.Value;
    }

    public static implicit operator PaymentId(Guid guid)
    {
        return new PaymentId(guid);
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
