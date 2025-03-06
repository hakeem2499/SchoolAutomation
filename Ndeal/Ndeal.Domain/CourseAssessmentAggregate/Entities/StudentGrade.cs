using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel; // Assuming Result and Error are here
using Ndeal.SharedKernel.ValueObjects;
using SharedKernel; // Assuming GradePoint is here

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class StudentGrade : Entity<StudentGradeId>
{
    // Private constructor for controlled creation
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
        Grade = CalculateGrade(totalScore); // Calculate GradePoint on creation
    }

    // Properties with private setters for encapsulation
    public StudentId StudentId { get; private set; }
    public AssessmentResultId AssessmentResultId { get; private set; }
    public decimal TotalScore { get; private set; }
    public GradePoint Grade { get; private set; }

    // Factory method with validation
    public static Result<StudentGrade> Create(
        StudentId studentId,
        AssessmentResultId assessmentResultId,
        decimal totalScore
    )
    {
        if (studentId is null)
        {
            return Result.Failure<StudentGrade>(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (assessmentResultId is null)
        {
            return Result.Failure<StudentGrade>(
                Error.Validation("AssessmentResultId.Required", "Assessment result ID is required.")
            );
        }

        if (totalScore < 0 || totalScore > 100)
        {
            return Result.Failure<StudentGrade>(
                Error.Validation("TotalScore.Invalid", "Total score must be between 0 and 100.")
            );
        }

        var grade = new StudentGrade(
            StudentGradeId.NewStudentGradeId(),
            studentId,
            assessmentResultId,
            totalScore
        );
        // Optional: Raise a domain event
        // grade.Raise(new StudentGradeCreatedEvent(grade.Id, studentId, totalScore, grade.Grade));
        return Result.Success(grade);
    }

    // Update method with validation
    public Result UpdateTotalScore(decimal newTotalScore)
    {
        if (newTotalScore < 0 || newTotalScore > 100)
        {
            return Result.Failure(
                Error.Validation("TotalScore.Invalid", "Total score must be between 0 and 100.")
            );
        }

        TotalScore = newTotalScore;
        Grade = CalculateGrade(newTotalScore);
        // Optional: Raise a domain event
        // Raise(new StudentGradeUpdatedEvent(Id, StudentId, newTotalScore, Grade));
        return Result.Success();
    }

    // Private method to calculate GradePoint
    private static GradePoint CalculateGrade(decimal totalScore)
    {
        GradePoint.LetterGrade letterGrade = totalScore switch
        {
            >= 90 => GradePoint.LetterGrade.A,
            >= 80 => GradePoint.LetterGrade.B,
            >= 70 => GradePoint.LetterGrade.C,
            >= 60 => GradePoint.LetterGrade.D,
            _ => GradePoint.LetterGrade.F,
        };

        return new GradePoint(letterGrade);
    }
}
