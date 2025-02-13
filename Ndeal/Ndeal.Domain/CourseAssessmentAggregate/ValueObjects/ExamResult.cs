using Ndeal.SharedKernel;

namespace Ndeal.Domain.AssessmentAggregate.ValueObjects;

public class ExamResultId : ValueObject
{
    public Guid Value { get; }

    public ExamResultId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ExamResultId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ExamResultId NewExamResultId()
    {
        return new ExamResultId(Guid.NewGuid());
    }

    public static ExamResultId FromGuid(Guid guid)
    {
        return new ExamResultId(guid);
    }

    public static implicit operator Guid(ExamResultId ExamResultId)
    {
        return ExamResultId.Value;
    }

    public static implicit operator ExamResultId(Guid guid)
    {
        return new ExamResultId(guid);
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
