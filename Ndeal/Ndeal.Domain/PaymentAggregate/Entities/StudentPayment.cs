using Ndeal.Domain.PaymentAggregate.Enums;
using Ndeal.Domain.PaymentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.Entities;

public class StudentPayment : Entity<StudentPaymentId>
{
    private readonly List<Invoice> _invoices = new();
    private readonly List<Receipt> _receipts = new();

    public StudentPayment(
        StudentPaymentId studentPaymentId,
        StudentId studentId,
        PaymentId paymentId
    )
        : base(studentPaymentId)
    {
        StudentPaymentId = studentPaymentId;
        StudentId = studentId;
        PaymentId = paymentId;
    }

    public StudentPaymentId StudentPaymentId { get; }
    public StudentId StudentId { get; }
    public PaymentId PaymentId { get; }

    public IReadOnlyList<Invoice> Invoices => _invoices;
    public IReadOnlyList<Receipt> Receipts => _receipts;

    internal static StudentPayment Create(StudentId studentId, PaymentId paymentId)
    {
        return new StudentPayment(StudentPaymentId.NewStudentPaymentId(), studentId, paymentId);
    }

    internal void AddInvoice(Money amount, DateTime dueDate)
    {
        var invoice = Invoice.Create(Id, amount, dueDate);
        _invoices.Add(invoice);
    }

    internal void UpdateInvoice(InvoiceId invoiceId, Money amount, DateTime dueDate)
    {
        Invoice? invoice = _invoices.Single(x => x.Id == invoiceId);
        invoice.Update(amount, dueDate);
    }

    internal void AddReceipt(
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
    {
        var receipt = Receipt.Create(Id, amount, paymentDate, paymentMethodType);
        _receipts.Add(receipt);
    }

    internal void UpdateReceipt(
        ReceiptId receiptId,
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
    {
        Receipt? receipt = _receipts.Single(x => x.Id == receiptId);
        receipt.Update(amount, paymentDate, paymentMethodType);
    }

    internal void RemoveInvoice(
        
    )
}
