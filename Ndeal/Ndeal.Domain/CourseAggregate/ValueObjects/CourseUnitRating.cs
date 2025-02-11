using Ndeal.Domain.CourseAggregate.Enums;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAggregate.ValueObjects;

public class CourseUnitRating : ValueObject
{
    public CourseUnitRating(CourseUnit courseUnit)
    {
        CourseUnit = courseUnit;
    }

    public CourseUnit CourseUnit { get; private set; }

    // Optional: Get numeric representation (if needed)
    public int GetNumericValue() => (int)CourseUnit;

    public override string ToString()
    {
        return CourseUnit.ToString(); // Or a custom representation if needed
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CourseUnit;
    }
}
