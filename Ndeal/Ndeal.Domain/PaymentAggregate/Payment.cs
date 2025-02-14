using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.PaymentAggregate.Entities;
using Ndeal.Domain.PaymentAggregate.Enums;
using Ndeal.Domain.PaymentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.PaymentAggregate;

public class Payment : Entity<PaymentId>
{
    private readonly List<StudentPayment> _studentPayments = new();

    public Payment(
        PaymentId paymentId,
        DepartmentId departmentId,
        Money amount,
        DateTime dueDate,
        DateTime createdDate
    )
        : base(paymentId)
    {
        DepartmentId = departmentId;
        Amount = amount;
        DueDate = dueDate;
        CreatedDate = createdDate;
    }

    public DepartmentId DepartmentId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public IReadOnlyList<StudentPayment> StudentPayments => _studentPayments;

    public static Payment Create(
        DepartmentId departmentId,
        Money amount,
        DateTime dueDate,
        DateTime createdDate
    )
    {
        return new Payment(PaymentId.NewPaymentId(), departmentId, amount, dueDate, createdDate);
    }

    public void AddStudentPayment(StudentId studentId)
    {
        var studentPayment = StudentPayment.Create(studentId, Id);
        _studentPayments.Add(studentPayment);
    }

    public void AddInvoice(StudentPaymentId studentPaymentId, Money amount, DateTime dueDate)
    {
        StudentPayment studentPayment = _studentPayments.Single(x => x.Id == studentPaymentId);
        studentPayment.AddInvoice(amount, dueDate);
    }

    public void UpdateInvoice(
        StudentPaymentId studentPaymentId,
        InvoiceId invoiceId,
        Money amount,
        DateTime dueDate
    )
    {
        StudentPayment studentPayment = _studentPayments.Single(x => x.Id == studentPaymentId);
        studentPayment.UpdateInvoice(invoiceId, amount, dueDate);
    }

    public void AddReceipt(
        StudentPaymentId studentPaymentId,
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
    {
        StudentPayment studentPayment = _studentPayments.Single(x => x.Id == studentPaymentId);
        studentPayment.AddReceipt(amount, paymentDate, paymentMethodType);
    }

    public void UpdateReceipt(
        StudentPaymentId studentPaymentId,
        ReceiptId receiptId,
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
    {
        StudentPayment studentPayment = _studentPayments.Single(x => x.Id == studentPaymentId);
        studentPayment.UpdateReceipt(receiptId, amount, paymentDate, paymentMethodType);
    }

    public void RemoveStudentPayment(StudentPaymentId studentPaymentId)
    {
        StudentPayment studentPayment = _studentPayments.Single(x => x.Id == studentPaymentId);
        _studentPayments.Remove(studentPayment);
    }
}
