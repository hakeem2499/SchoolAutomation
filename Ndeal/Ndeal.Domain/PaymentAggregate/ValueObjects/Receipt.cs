using Ndeal.SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.ValueObjects;

public class ReceiptId : ValueObject
{
    public Guid Value { get; }

    public ReceiptId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ReceiptId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ReceiptId NewReceiptId()
    {
        return new ReceiptId(Guid.NewGuid());
    }

    public static ReceiptId FromGuid(Guid guid)
    {
        return new ReceiptId(guid);
    }

    public static implicit operator Guid(ReceiptId ReceiptId)
    {
        return ReceiptId.Value;
    }

    public static implicit operator ReceiptId(Guid guid)
    {
        return new ReceiptId(guid);
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
