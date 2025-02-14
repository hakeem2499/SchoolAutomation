using Ndeal.Domain.PaymentAggregate.Enums;
using Ndeal.Domain.PaymentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.Entities;

public class Invoice : Entity<InvoiceId>
{
    public Invoice(
        InvoiceId invoiceId,
        StudentPaymentId studentPaymentId,
        Money amount,
        DateTime dueDate,
        PaymentStatus paymentStatus = PaymentStatus.Pending
    )
        : base(invoiceId)
    {
        StudentPaymentId = studentPaymentId;
        Amount = amount;
        DueDate = dueDate;
        PaymentStatus = paymentStatus;
    }

    public StudentPaymentId StudentPaymentId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }

    internal static Invoice Create(
        StudentPaymentId studentPaymentId,
        Money amount,
        DateTime dueDate
    )
    {
        return new Invoice(InvoiceId.NewInvoiceId(), studentPaymentId, amount, dueDate);
    }

    internal void Update(Money amount, DateTime dueDate)
    {
        Amount = amount;
        DueDate = dueDate;
    }
}
