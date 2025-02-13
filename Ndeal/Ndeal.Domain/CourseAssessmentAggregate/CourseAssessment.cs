using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.Entities;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate;

public class CourseAssessment(
    CourseAssessmentId id,
    CourseId courseId,
    SemesterId semesterId,
    string name
) : AggregateRoot<CourseAssessmentId>(id)
{
    private readonly List<Test> _tests = new();
    private readonly List<AssessmentResult> _assessmentResults = new();

    public CourseId CourseId { get; private set; } = courseId;
    public SemesterId SemesterId { get; private set; } = semesterId;
    public string Name { get; private set; } = name;

    public IReadOnlyList<Test> Tests => _tests.AsReadOnly();
    public Exam? Exam { get; private set; }
    public IReadOnlyList<AssessmentResult> AssessmentResults => _assessmentResults.AsReadOnly();

    public static CourseAssessment CreateCourseAssessment(
        CourseId courseId,
        SemesterId semesterId,
        string name
    )
    {
        return new CourseAssessment(
            CourseAssessmentId.NewCourseAssessmentId(),
            courseId,
            semesterId,
            name
        );
    }

    public void AddTest(AssessmentId assessmentId, int resultWeight, int maxScore, string name)
    {
        var test = Test.CreateTest(assessmentId, resultWeight, maxScore, name);
        _tests.Add(test);
    }

    public void AddExam(AssessmentId assessmentId, int resultWeight, int maxScore, string name)
    {
        if (Exam is not null)
        {
            throw new InvalidOperationException("Exam already exists.");
        }

        var exam = Exam.CreateExam(assessmentId, resultWeight, maxScore, name);
        Exam = exam;
    }

    public void UpdateExam(int resultWeight, int maxScore, string name)
    {
        if (Exam is null)
        {
            throw new InvalidOperationException("Exam does not exist.");
        }

        Exam.UpdateExam(resultWeight, maxScore, name);
    }

    public void UpdateTest(TestId testId, int resultWeight, int maxScore, string name)
    {
        Test? test =
            _tests.FirstOrDefault(t => t.Id == testId)
            ?? throw new InvalidOperationException("Test does not exist.");

        test.UpdateTest(resultWeight, maxScore, name);
    }
}
