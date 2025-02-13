using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.AssessmentAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class AssessmentResult : Entity<AssessmentResultId>
{
    private readonly List<TestResult> _testResults = new();
    private readonly List<ExamResult> _examResults = new();
    private readonly List<StudentGrades> _studentGrades = new();

    public AssessmentResult(
        AssessmentResultId id,
        CourseAssessmentId courseAssessmentId,
        DepartmentId departmentId,
        int maxScore,
        string name
    )
        : base(id)
    {
        CourseAssessmentId = courseAssessmentId;
        DepartmentId = departmentId;
        MaxScore = maxScore;
        Name = name;
    }

    public CourseAssessmentId CourseAssessmentId { get; private set; }

    public DepartmentId DepartmentId { get; private set; }
    public int MaxScore { get; private set; }
    public string Name { get; private set; }

    public IReadOnlyList<TestResult> TestResults => _testResults.AsReadOnly();
    public IReadOnlyList<ExamResult> ExamResults => _examResults.AsReadOnly();
    public IReadOnlyList<StudentGrades> StudentGrades => _studentGrades.AsReadOnly();

    internal static AssessmentResult CreateAssessmentResult(
        CourseAssessmentId CourseAssessmentId,
        DepartmentId departmentId,
        int maxScore,
        string name
    )
    {
        return new AssessmentResult(
            AssessmentResultId.NewAssessmentResultId(),
            CourseAssessmentId,
            departmentId,
            maxScore,
            name
        );
    }

    public decimal GetTotalScoreForStudent(StudentId studentId)
    {
        decimal totalTestScore = _testResults
            .Where(tr => tr.StudentId == studentId)
            .Sum(tr => tr.Score);

        decimal examScore =
            _examResults.FirstOrDefault(er => er.StudentId == studentId)?.Score ?? 0; // Handle case where no exam result exists

        return totalTestScore + examScore;
    }

    public void AddTestResult(StudentId studentId, TestId testId, decimal score)
    {
        var testResult = TestResult.CreateTestResult(testId, studentId, Id, score);
        _testResults.Add(testResult);
        //Raise(new TestResultAddedEvent(Id, testResult.Id, studentId, score)); // Raise Domain Event
    }

    public void AddExamResult(StudentId studentId, ExamId examId, decimal score)
    {
        // Check if ExamResult already exists for this student
        var existingExamResult = _examResults.FirstOrDefault(er => er.StudentId == studentId);
        if (existingExamResult != null)
        {
            throw new InvalidOperationException(
                $"Exam result already exists for student {studentId}."
            );
        }

        var examResult = ExamResult.CreateExamResult(examId, studentId, Id, score);
        _examResults.Add(examResult);
        //Raise(new ExamResultSetEvent(Id, examResult.Id, studentId, score)); // Raise Domain Event
    }
}
