using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;

public class AssessmentResultId : ValueObject
{
    private static int _lastId = 0; // Static field to keep track of the last used ID

    public int Value { get; }

    public AssessmentResultId(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException(
                "AssessmentResultId must be a positive integer",
                nameof(value)
            );
        }

        Value = value;
    }

    public static AssessmentResultId NewAssessmentResultId()
    {
        _lastId++; // Increment the last used ID
        return new AssessmentResultId(_lastId);
    }

    public static implicit operator int(AssessmentResultId AssessmentResultId)
    {
        return AssessmentResultId.Value;
    }

    public static implicit operator AssessmentResultId(int value)
    {
        return new AssessmentResultId(value);
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
