using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.Entities;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
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

    public CourseId CourseId { get; private set; } = courseId;
    public SemesterId SemesterId { get; private set; } = semesterId;
    public string Name { get; private set; } = name;

    public IReadOnlyList<Test> Tests => _tests.AsReadOnly();
    public Exam? Exam { get; private set; }

    public AssessmentResult? AssessmentResult { get; private set; }

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

    public void AddTest(
        CourseAssessmentId courseAssessmentId,
        int resultWeight,
        int maxScore,
        string name
    )
    {
        var test = Test.CreateTest(courseAssessmentId, resultWeight, maxScore, name);
        _tests.Add(test);
    }

    public void AddExam(
        CourseAssessmentId courseAssessmentId,
        int resultWeight,
        int maxScore,
        string name
    )
    {
        if (Exam is not null)
        {
            throw new InvalidOperationException("Exam already exists.");
        }

        var exam = Exam.CreateExam(courseAssessmentId, resultWeight, maxScore, name);
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

    public void AddAssessmentResult(DepartmentId departmentId, int maxScore, string name)
    {
        if (AssessmentResult is not null)
        {
            throw new InvalidOperationException("Assessment result already exists.");
        }

        var assessmentResult = AssessmentResult.CreateAssessmentResult(
            Id,
            departmentId,
            maxScore,
            name
        );
        AssessmentResult = assessmentResult;
    }

    public void AddTestResult(StudentId studentId, TestId testId, decimal score)
    {
        if (AssessmentResult is null)
        {
            throw new InvalidOperationException("Assessment result does not exist.");
        }
        Test test =
            _tests.FirstOrDefault(t => t.Id == testId)
            ?? throw new InvalidOperationException("Test does not exist.");
        decimal testMarkObtained = score / test.MaxScore * test.ResultWeight;

        AssessmentResult.AddTestResult(studentId, testId, testMarkObtained);
    }

    public void AddExamResult(StudentId studentId, decimal score)
    {
        if (AssessmentResult is null)
        {
            throw new InvalidOperationException("Assessment result does not exist.");
        }

        if (Exam is null)
        {
            throw new InvalidOperationException("Exam does not exist.");
        }

        decimal examMarkObtained = score / Exam.MaxScore * Exam.ResultWeight;

        AssessmentResult.AddExamResult(studentId, Exam.Id, examMarkObtained);
    }

    public void AddStudentGrade(StudentId studentId)
    {
        if (AssessmentResult is null)
        {
            throw new InvalidOperationException("Assessment result does not exist.");
        }

        AssessmentResult.AddStudentGrade(studentId);
    }
}
