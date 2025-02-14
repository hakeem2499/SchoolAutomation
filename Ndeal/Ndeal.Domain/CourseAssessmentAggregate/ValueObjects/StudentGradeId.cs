using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;

public class StudentGradeId : ValueObject
{
    public Guid Value { get; }

    public StudentGradeId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("StudentGradeId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static StudentGradeId NewStudentGradeId()
    {
        return new StudentGradeId(Guid.NewGuid());
    }

    public static StudentGradeId FromGuid(Guid guid)
    {
        return new StudentGradeId(guid);
    }

    public static implicit operator Guid(StudentGradeId StudentGradeId)
    {
        return StudentGradeId.Value;
    }

    public static implicit operator StudentGradeId(Guid guid)
    {
        return new StudentGradeId(guid);
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
