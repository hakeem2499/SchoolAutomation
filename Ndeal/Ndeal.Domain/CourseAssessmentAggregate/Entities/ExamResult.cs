using Ndeal.Domain.AssessmentAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class ExamResult : Entity<ExamResultId>
{
    public ExamResult(
        ExamResultId id,
        StudentId studentId,
        ExamId examId,
        AssessmentResultId assessmentResultId,
        decimal score
    )
        : base(id)
    {
        ExamId = examId;
        Score = score;
        StudentId = studentId;
        AssessmentResultId = assessmentResultId;
    }

    public ExamId ExamId { get; private set; }
    public decimal Score { get; private set; }

    public StudentId StudentId { get; private set; }
    public AssessmentResultId AssessmentResultId { get; private set; }

    internal static ExamResult CreateExamResult(
        ExamId examId,
        StudentId studentId,
        AssessmentResultId assessmentResultId,
        decimal score
    )
    {
        return new ExamResult(
            ExamResultId.NewExamResultId(),
            studentId,
            examId,
            assessmentResultId,
            score
        );
    }

    internal void UpdateExamResult(int score)
    {
        Score = score;
    }
}
