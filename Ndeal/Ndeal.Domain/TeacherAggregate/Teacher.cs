using Ndeal.Domain.TeacherAggregate.Entities;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.Domain.UserAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.TeacherAggregate;

public class Teacher : AggregateRoot<TeacherId>
{
    private readonly List<Qualification> _qualifications = new();

    public Teacher(
        TeacherId id,
        UserId userId,
        string firstName,
        string email,
        string phone,
        string address,
        string lastName
    )
        : base(id)
    {
        UserId = userId;
        FirstName = firstName;
        Email = email;
        Phone = phone;
        Address = address;
        LastName = lastName;
    }

    public UserId UserId { get; private set; }
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string LastName { get; private set; }

    public IReadOnlyCollection<Qualification> Qualifications => _qualifications.AsReadOnly();

    public static Teacher Create(
        UserId userId,
        string firstName,
        string email,
        string phone,
        string address,
        string lastName
    )
    {
        return new Teacher(
            TeacherId.NewTeacherId(),
            userId,
            firstName,
            email,
            phone,
            address,
            lastName
        );
    }

    public void CreateQualification(
        TeacherId teacherId,
        string name,
        string title,
        DateTime dateObtained,
        string institution
    )
    {
        var qualification = Qualification.Create(teacherId, name, title, dateObtained, institution);

        _qualifications.Add(qualification);
    }

    public void RemoveQualification(QualificationId qualificationId)
    {
        Qualification? qualification =
            _qualifications.Find(q => q.Id == qualificationId)
            ?? throw new ArgumentException(
                "qualification cannot be a null value ",
                nameof(qualificationId)
            );
        _qualifications.Remove(qualification);
    }
}
