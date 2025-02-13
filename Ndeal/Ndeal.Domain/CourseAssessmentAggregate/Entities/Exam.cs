using Ndeal.Domain.AssessmentAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class Exam : Entity<ExamId>
{
    public Exam(
        ExamId id,
        CourseAssessmentId courseAssessmentId,
        int resultWeight,
        int maxScore,
        string name
    )
        : base(id)
    {
        CourseAssessmentId = courseAssessmentId;
        MaxScore = maxScore;
        Name = name;
        ResultWeight = resultWeight;
    }

    public CourseAssessmentId CourseAssessmentId { get; private set; }
    public int MaxScore { get; private set; }
    public string Name { get; private set; }

    public int ResultWeight { get; private set; }

    internal static Exam CreateExam(
        CourseAssessmentId courseAssessmentId,
        int resultWeight,
        int maxScore,
        string name
    )
    {
        return new Exam(ExamId.NewExamId(), courseAssessmentId, resultWeight, maxScore, name);
    }

    internal void UpdateExam(int resultWeight, int maxScore, string name)
    {
        ResultWeight = resultWeight;
        MaxScore = maxScore;
        Name = name;
    }
}
