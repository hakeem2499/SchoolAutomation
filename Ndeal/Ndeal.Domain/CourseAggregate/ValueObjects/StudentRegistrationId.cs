using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate.ValueObjects;

public class StudentRegistrationId : ValueObject
{
    public Guid Value { get; }

    public StudentRegistrationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("StudentRegistrationId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static StudentRegistrationId NewStudentRegistrationId()
    {
        return new StudentRegistrationId(Guid.NewGuid());
    }

    public static StudentRegistrationId FromGuid(Guid guid)
    {
        return new StudentRegistrationId(guid);
    }

    public static implicit operator Guid(StudentRegistrationId StudentRegistrationId)
    {
        return StudentRegistrationId.Value;
    }

    public static implicit operator StudentRegistrationId(Guid guid)
    {
        return new StudentRegistrationId(guid);
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
