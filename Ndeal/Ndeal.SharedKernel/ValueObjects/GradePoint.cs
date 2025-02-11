namespace Ndeal.SharedKernel.ValueObjects;

public class GradePoint : ValueObject
{
    public enum LetterGrade
    {
        A,
        B,
        C,
        D,
        E,
        F,
    }

    public LetterGrade Value { get; }

    public GradePoint(LetterGrade value)
    {
        Value = value;
    }

    public decimal GetNumericValue()
    {
        return Value switch
        {
            LetterGrade.A => 5.0m,
            LetterGrade.B => 4.0m,
            LetterGrade.C => 3.0m,
            LetterGrade.D => 2.0m,
            LetterGrade.E => 1.0m,
            LetterGrade.F => 0.0m,
            _ => throw new InvalidOperationException("Invalid letter grade."),
        };
    }

    public string GetRemark()
    {
        return Value switch
        {
            LetterGrade.A => "Distinction",
            LetterGrade.B => "Excellent",
            LetterGrade.C => "Good",
            LetterGrade.D => "Fair",
            LetterGrade.E => "Poor",
            LetterGrade.F => "Fail",
            _ => throw new InvalidOperationException("Invalid letter grade."),
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
