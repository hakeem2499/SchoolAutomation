using Ndeal.Domain.PaymentAggregate.Enums;
using Ndeal.Domain.PaymentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.Entities;

public class Receipt : Entity<ReceiptId>
{
    public Receipt(
        ReceiptId receiptId,
        StudentPaymentId studentPaymentId,
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
        : base(receiptId)
    {
        StudentPaymentId = studentPaymentId;
        Amount = amount;
        PaymentDate = paymentDate;
        PaymentMethodType = paymentMethodType;
    }

    public StudentPaymentId StudentPaymentId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public PaymentMethodType PaymentMethodType { get; private set; }

    internal static Receipt Create(
        StudentPaymentId studentPaymentId,
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
    {
        return new Receipt(
            ReceiptId.NewReceiptId(),
            studentPaymentId,
            amount,
            paymentDate,
            paymentMethodType
        );
    }

    internal void Update(Money amount, DateTime paymentDate, PaymentMethodType paymentMethodType)
    {
        Amount = amount;
        PaymentDate = paymentDate;
        PaymentMethodType = paymentMethodType;
    }
}
