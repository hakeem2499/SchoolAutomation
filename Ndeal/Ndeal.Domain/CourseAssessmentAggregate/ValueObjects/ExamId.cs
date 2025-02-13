using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;

public class ExamId : ValueObject
{
    private static int _lastId = 0; // Static field to keep track of the last used ID

    public int Value { get; }

    public ExamId(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("ExamId must be a positive integer", nameof(value));
        }

        Value = value;
    }

    public static ExamId NewExamId()
    {
        _lastId++; // Increment the last used ID
        return new ExamId(_lastId);
    }

    public static implicit operator int(ExamId ExamId)
    {
        return ExamId.Value;
    }

    public static implicit operator ExamId(int value)
    {
        return new ExamId(value);
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
