using System;
using System.Collections.Generic;
using System.Linq;
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
        StudentPaymentId =
            studentPaymentId ?? throw new ArgumentNullException(nameof(studentPaymentId));
        StudentId = studentId ?? throw new ArgumentNullException(nameof(studentId));
        PaymentId = paymentId ?? throw new ArgumentNullException(nameof(paymentId));
    }

    public StudentPaymentId StudentPaymentId { get; }
    public StudentId StudentId { get; }
    public PaymentId PaymentId { get; }

    public IReadOnlyCollection<Invoice> Invoices => _invoices.AsReadOnly();
    public IReadOnlyCollection<Receipt> Receipts => _receipts.AsReadOnly();

    public static StudentPayment Create(StudentId studentId, PaymentId paymentId)
    {
        if (studentId == null)
        {
            throw new ArgumentNullException(nameof(studentId));
        }

        if (paymentId == null)
        {
            throw new ArgumentNullException(nameof(paymentId));
        }

        return new StudentPayment(StudentPaymentId.NewStudentPaymentId(), studentId, paymentId);
    }

    public void AddInvoice(Money amount, DateTime dueDate)
    {
        if (amount == null)
        {
            throw new ArgumentNullException(nameof(amount));
        }

        if (dueDate == default)
        {
            throw new ArgumentException("Due date must be provided.", nameof(dueDate));
        }

        var invoice = Invoice.Create(Id, amount, dueDate);
        _invoices.Add(invoice);
    }

    public void UpdateInvoice(InvoiceId invoiceId, Money amount, DateTime dueDate)
    {
        if (invoiceId == null)
        {
            throw new ArgumentNullException(nameof(invoiceId));
        }

        if (amount == null)
        {
            throw new ArgumentNullException(nameof(amount));
        }

        if (dueDate == default)
        {
            throw new ArgumentException("Due date must be provided.", nameof(dueDate));
        }

        Invoice invoice =
            _invoices.SingleOrDefault(x => x.Id == invoiceId)
            ?? throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");

        invoice.Update(amount, dueDate);
    }

    public void AddReceipt(Money amount, DateTime paymentDate, PaymentMethodType paymentMethodType)
    {
        if (amount == null)
        {
            throw new ArgumentNullException(nameof(amount));
        }

        if (paymentDate == default)
        {
            throw new ArgumentException("Payment date must be provided.", nameof(paymentDate));
        }

        var receipt = Receipt.Create(Id, amount, paymentDate, paymentMethodType);
        _receipts.Add(receipt);
    }

    public void UpdateReceipt(
        ReceiptId receiptId,
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
    {
        if (receiptId == null)
        {
            throw new ArgumentNullException(nameof(receiptId));
        }

        if (amount == null)
        {
            throw new ArgumentNullException(nameof(amount));
        }

        if (paymentDate == default)
        {
            throw new ArgumentException("Payment date must be provided.", nameof(paymentDate));
        }

        Receipt receipt =
            _receipts.SingleOrDefault(x => x.Id == receiptId)
            ?? throw new InvalidOperationException($"Receipt with ID {receiptId} not found.");

        receipt.Update(amount, paymentDate, paymentMethodType);
    }

    public void DeleteReceipt(ReceiptId receiptId)
    {
        if (receiptId == null)
        {
            throw new ArgumentNullException(nameof(receiptId));
        }

        Receipt receipt =
            _receipts.SingleOrDefault(x => x.Id == receiptId)
            ?? throw new InvalidOperationException($"Receipt with ID {receiptId} not found.");

        _receipts.Remove(receipt);
    }

    public void DeleteInvoice(InvoiceId invoiceId)
    {
        if (invoiceId == null)
        {
            throw new ArgumentNullException(nameof(invoiceId));
        }

        Invoice invoice =
            _invoices.SingleOrDefault(x => x.Id == invoiceId)
            ?? throw new InvalidOperationException($"Invoice with ID {invoiceId} not found.");

        _invoices.Remove(invoice);
    }
}
