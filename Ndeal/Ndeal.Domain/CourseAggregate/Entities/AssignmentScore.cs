using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate.Entities;

public class AssignmentScore : Entity<AssignmentScoreId>
{
    public AssignmentScore(
        AssignmentScoreId id,
        Assignment assignment,
        StudentId studentId,
        AssessmentScore score
    )
        : base(id)
    {
        Id = id;
        AssignmentId = assignment.Id;
        MaxScore = assignment.MaxScore;
        StudentId = studentId;
        Score = score;
    }

    public AssignmentId AssignmentId { get; private set; }
    public StudentId StudentId { get; private set; }
    public AssessmentScore Score { get; private set; }
    public int MaxScore { get; private set; }

    internal static AssignmentScore Create(Assignment assignment, StudentId studentId, int score)
    {
        AssessmentScore assessmentScore = new(score, assignment.MaxScore);
        return new AssignmentScore(
            AssignmentScoreId.NewAssignmentScoreId(),
            assignment,
            studentId,
            assessmentScore
        );
    }

    internal void UpdateScore(int score)
    {
        Score = new AssessmentScore(score, MaxScore);
    }
}
