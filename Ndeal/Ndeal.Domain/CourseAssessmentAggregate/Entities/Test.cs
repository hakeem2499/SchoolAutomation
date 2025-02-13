using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class Test : Entity<TestId>
{
    public Test(TestId id, AssessmentId assessmentId, int resultWeight, int maxScore, string name)
        : base(id)
    {
        AssessmentId = assessmentId;
        MaxScore = maxScore;
        Name = name;
        ResultWeight = resultWeight;
    }

    public AssessmentId AssessmentId { get; private set; }
    public int MaxScore { get; private set; }
    public string Name { get; private set; }

    public int ResultWeight { get; private set; }

    internal static Test CreateTest(
        AssessmentId assessmentId,
        int resultWeight,
        int maxScore,
        string name
    )
    {
        return new Test(TestId.NewTestId(), assessmentId, resultWeight, maxScore, name);
    }

    internal void UpdateTest(int resultWeight, int maxScore, string name)
    {
        ResultWeight = resultWeight;
        MaxScore = maxScore;
        Name = name;
    }
}
