using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel.ValueObjects;
using SharedKernel; // Assuming GradePoint is in this namespace

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities; // Or your appropriate namespace

public class StudentGrade : Entity<StudentGradeId>
{
    public StudentId StudentId { get; private set; }
    public AssessmentResultId AssessmentResultId { get; private set; }
    public decimal TotalScore { get; private set; }
    public GradePoint Grade { get; private set; }

    private StudentGrade(
        StudentGradeId id,
        StudentId studentId,
        AssessmentResultId assessmentResultId,
        decimal totalScore
    )
        : base(id)
    {
        StudentId = studentId;
        AssessmentResultId = assessmentResultId;
        TotalScore = totalScore;
        Grade = new GradePoint(GradePoint.LetterGrade.F); // Initialize Grade with a default value
        CalculateGrade(); // Calculate GradePoint when the entity is created
    }

    public static StudentGrade Create(
        StudentId studentId,
        AssessmentResultId assessmentResultId,
        decimal totalScore
    )
    {
        // Add validation here (e.g., totalScore within valid range)
        if (totalScore < 0 || totalScore > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(totalScore),
                "Total score must be between 0 and 100."
            );
        }

        return new StudentGrade(
            StudentGradeId.NewStudentGradeId(),
            studentId,
            assessmentResultId,
            totalScore
        );
    }

    public void UpdateTotalScore(decimal newTotalScore)
    {
        if (newTotalScore < 0 || newTotalScore > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(newTotalScore),
                "Total score must be between 0 and 100."
            );
        }
        TotalScore = newTotalScore;
        CalculateGrade(); // Recalculate GradePoint
    }

    private void CalculateGrade()
    {
        // Determine the letter grade based on the total score (example ranges)
        GradePoint.LetterGrade letterGrade = TotalScore switch
        {
            >= 90 => GradePoint.LetterGrade.A,
            >= 80 => GradePoint.LetterGrade.B,
            >= 70 => GradePoint.LetterGrade.C,
            >= 60 => GradePoint.LetterGrade.D,
            _ => GradePoint.LetterGrade.F,
        };

        Grade = new GradePoint(letterGrade); // Create a new GradePoint value object
    }
}
