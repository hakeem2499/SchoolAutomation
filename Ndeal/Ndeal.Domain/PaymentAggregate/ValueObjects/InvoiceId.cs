using Ndeal.SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.ValueObjects;

public class InvoiceId : ValueObject
{
    public Guid Value { get; }

    public InvoiceId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("InvoiceId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static InvoiceId NewInvoiceId()
    {
        return new InvoiceId(Guid.NewGuid());
    }

    public static InvoiceId FromGuid(Guid guid)
    {
        return new InvoiceId(guid);
    }

    public static implicit operator Guid(InvoiceId InvoiceId)
    {
        return InvoiceId.Value;
    }

    public static implicit operator InvoiceId(Guid guid)
    {
        return new InvoiceId(guid);
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
