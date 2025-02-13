using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class Test : Entity<TestId>
{
    public Test(
        TestId id,
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

    internal static Test CreateTest(
        CourseAssessmentId courseAssessmentId,
        int resultWeight,
        int maxScore,
        string name
    )
    {
        return new Test(TestId.NewTestId(), courseAssessmentId, resultWeight, maxScore, name);
    }

    internal void UpdateTest(int resultWeight, int maxScore, string name)
    {
        ResultWeight = resultWeight;
        MaxScore = maxScore;
        Name = name;
    }
}
