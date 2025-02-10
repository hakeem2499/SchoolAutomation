using Ndeal.Domain.AssessmentAggregate.ValueObjects;
using Ndeal.Domain.AttendanceAggregate.ValueObjects;
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.PaymentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.Entities;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.UserAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.StudentAggregate;

public sealed class Student : AggregateRoot<StudentId>
{
    private readonly List<CourseId> _courseIds = new();
    private readonly List<AssessmentId> _assessmentIds = new();
    private readonly List<AttendanceId> _attendanceIds = new();
    private readonly List<PaymentId> _paymentIds = new();
    public UserId UserId { get; private init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Address { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Nationality { get; private set; }

    public Enrollment? Enrollment { get; private set; } // Make Enrollment nullable and add a setter
    public Guardian? Guardian { get; private set; } // Make Guardian nullable and add a setter

    public IReadOnlyList<CourseId> CourseIds => _courseIds.AsReadOnly();
    public IReadOnlyList<AssessmentId> AssessmentIds => _assessmentIds.AsReadOnly();
    public IReadOnlyList<AttendanceId> AttendanceIds => _attendanceIds.AsReadOnly();
    public IReadOnlyList<PaymentId> PaymentIds => _paymentIds.AsReadOnly();

    private Student(
        StudentId studentId,
        UserId userId,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string address,
        DateTime dateOfBirth,
        string nationality
    )
        : base(studentId)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
    }

    public static Student Create(
        UserId userId,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string address,
        DateTime dateOfBirth,
        string nationality
    )
    {
        return new(
            StudentId.NewStudentId(),
            userId,
            firstName,
            lastName,
            email,
            phoneNumber,
            address,
            dateOfBirth,
            nationality
        );
    }

    public void Enroll(Department department)
    {
        if (Enrollment is not null)
        {
            throw new InvalidOperationException("Student is already enrolled."); //Guard clause
        }
        DateTime enrollmentDate = DateTime.UtcNow;
        Enrollment = new Enrollment(
            EnrollmentId.NewEnrollmentId(),
            department,
            this,
            enrollmentDate
        );
    }

    public void AddGuardian(string name, string phoneNumber, string email)
    {
        if (Guardian is not null)
        {
            throw new InvalidOperationException("Student already has a guardian."); //Guard clause
        }
        Guardian = new Guardian(GuardianId.NewGuardianId(), name, phoneNumber, email);
    }

    //To Consider adding an Update method for Student properties
}
