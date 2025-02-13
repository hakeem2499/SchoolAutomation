using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.AssessmentAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class TestResult : Entity<TestResultId>
{
    public TestResult(
        TestResultId id,
        StudentId studentId,
        AssessmentResultId assessmentResultId,
        TestId testId,
        decimal score
    )
        : base(id)
    {
        TestId = testId;
        Score = score;
        StudentId = studentId;
        AssessmentResultId = assessmentResultId;
    }

    public TestId TestId { get; private set; }
    public decimal Score { get; private set; }

    public StudentId StudentId { get; private set; }
    public AssessmentResultId AssessmentResultId { get; private set; }

    internal static TestResult CreateTestResult(
        TestId testId,
        StudentId studentId,
        AssessmentResultId assessmentResultId,
        decimal score
    )
    {
        return new TestResult(
            TestResultId.NewTestResultId(),
            studentId,
            assessmentResultId,
            testId,
            score
        );
    }

    internal void UpdateTestResult(int score)
    {
        Score = score;
    }
}
