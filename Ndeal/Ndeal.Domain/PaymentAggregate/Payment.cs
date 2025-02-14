using System;
using System.Collections.Generic;
using System.Linq;
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
        DepartmentId = departmentId ?? throw new ArgumentNullException(nameof(departmentId));
        Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        DueDate = dueDate;
        CreatedDate = createdDate;

        if (dueDate == default)
        {
            throw new ArgumentException("Due date must be provided.", nameof(dueDate));
        }

        if (createdDate == default)
        {
            throw new ArgumentException("Created date must be provided.", nameof(createdDate));
        }
    }

    public DepartmentId DepartmentId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public IReadOnlyCollection<StudentPayment> StudentPayments => _studentPayments.AsReadOnly();

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
        if (studentId == null)
        {
            throw new ArgumentNullException(nameof(studentId));
        }

        var studentPayment = StudentPayment.Create(studentId, Id);
        _studentPayments.Add(studentPayment);
    }

    public void AddInvoice(StudentPaymentId studentPaymentId, Money amount, DateTime dueDate)
    {
        if (studentPaymentId == null)
        {
            throw new ArgumentNullException(nameof(studentPaymentId));
        }

        StudentPayment studentPayment = GetStudentPayment(studentPaymentId);
        studentPayment.AddInvoice(amount, dueDate);
    }

    public void UpdateInvoice(
        StudentPaymentId studentPaymentId,
        InvoiceId invoiceId,
        Money amount,
        DateTime dueDate
    )
    {
        if (studentPaymentId == null)
        {
            throw new ArgumentNullException(nameof(studentPaymentId));
        }

        if (invoiceId == null)
        {
            throw new ArgumentNullException(nameof(invoiceId));
        }

        StudentPayment studentPayment = GetStudentPayment(studentPaymentId);
        studentPayment.UpdateInvoice(invoiceId, amount, dueDate);
    }

    public void AddReceipt(
        StudentPaymentId studentPaymentId,
        Money amount,
        DateTime paymentDate,
        PaymentMethodType paymentMethodType
    )
    {
        if (studentPaymentId == null)
        {
            throw new ArgumentNullException(nameof(studentPaymentId));
        }

        StudentPayment studentPayment = GetStudentPayment(studentPaymentId);
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
        if (studentPaymentId == null)
        {
            throw new ArgumentNullException(nameof(studentPaymentId));
        }

        if (receiptId == null)
        {
            throw new ArgumentNullException(nameof(receiptId));
        }

        StudentPayment studentPayment = GetStudentPayment(studentPaymentId);
        studentPayment.UpdateReceipt(receiptId, amount, paymentDate, paymentMethodType);
    }

    public void RemoveStudentPayment(StudentPaymentId studentPaymentId)
    {
        if (studentPaymentId == null)
        {
            throw new ArgumentNullException(nameof(studentPaymentId));
        }

        StudentPayment studentPayment = GetStudentPayment(studentPaymentId);
        _studentPayments.Remove(studentPayment);
    }

    public void RemoveInvoiceFromStudentPayment(
        StudentPaymentId studentPaymentId,
        InvoiceId invoiceId
    )
    {
        if (studentPaymentId == null)
        {
            throw new ArgumentNullException(nameof(studentPaymentId));
        }

        if (invoiceId == null)
        {
            throw new ArgumentNullException(nameof(invoiceId));
        }

        StudentPayment studentPayment = GetStudentPayment(studentPaymentId);
        studentPayment.DeleteInvoice(invoiceId);
    }

    public void RemoveReceiptFromStudentPayment(
        StudentPaymentId studentPaymentId,
        ReceiptId receiptId
    )
    {
        if (studentPaymentId == null)
        {
            throw new ArgumentNullException(nameof(studentPaymentId));
        }

        if (receiptId == null)
        {
            throw new ArgumentNullException(nameof(receiptId));
        }

        StudentPayment studentPayment = GetStudentPayment(studentPaymentId);
        studentPayment.DeleteReceipt(receiptId);
    }

    private StudentPayment GetStudentPayment(StudentPaymentId studentPaymentId)
    {
        return _studentPayments.SingleOrDefault(x => x.Id == studentPaymentId)
            ?? throw new InvalidOperationException(
                $"StudentPayment with ID {studentPaymentId} not found."
            );
    }
}
