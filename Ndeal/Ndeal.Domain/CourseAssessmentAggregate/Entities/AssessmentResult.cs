using System.Data.Common;
using System.Dynamic;
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
    private readonly List<StudentGrade> _studentGrades = new();

    public AssessmentResult(
        AssessmentResultId id,
        CourseAssessmentId courseAssessmentId,
        DepartmentId departmentId,
        int maxScore ,
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
    public IReadOnlyList<StudentGrade> StudentGrades => _studentGrades.AsReadOnly();

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

    internal void AddTestResult(StudentId studentId, TestId testId, decimal score)
    {
        var testResult = TestResult.CreateTestResult(testId, studentId, Id, score);
        _testResults.Add(testResult);
        //Raise(new TestResultAddedEvent(Id, testResult.Id, studentId, score)); // Raise Domain Event
    }

    internal void AddExamResult(StudentId studentId, ExamId examId, decimal score)
    {
        // Check if ExamResult already exists for this student
        ExamResult? existingExamResult = _examResults.FirstOrDefault(er =>
            er.StudentId == studentId
        );
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

    internal void AddStudentGrade(StudentId studentId)
    {
        decimal totalScore = GetTotalScoreForStudent(studentId);
        // Check if StudentGrade already exists for this student
        StudentGrade? existingStudentGrade = _studentGrades.FirstOrDefault(er =>
            er.StudentId == studentId
        );
        if (existingStudentGrade != null)
        {
            throw new InvalidOperationException(
                $"Student grade already exists for student {studentId}."
            );
        }

        var studentGrade = StudentGrade.Create(studentId, Id, totalScore);
        _studentGrades.Add(studentGrade);
        //Raise(new StudentGradeSetEvent(Id, studentGrade.Id, studentId, grade)); // Raise Domain Event
    }

    public void UpdateTestResult(TestId testId, decimal score)
    {
        TestResult? testResult =
            _testResults.FirstOrDefault(tr => tr.TestId == testId)
            ?? throw new InvalidOperationException("Test result does not exist.");

        testResult.UpdateTestResult(score);
        //Raise(new TestResultUpdatedEvent(Id, testResult.Id, testResult.StudentId, score)); // Raise Domain Event
    }

    public void UpdateExamResult(decimal score)
    {
        ExamResult? examResult =
            _examResults.FirstOrDefault()
            ?? throw new InvalidOperationException("Exam result does not exist.");

        examResult.UpdateExamResult(score);
        //Raise(new ExamResultUpdatedEvent(Id, examResult.Id, examResult.StudentId, score)); // Raise Domain Event
    }
}
