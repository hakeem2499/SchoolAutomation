using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class AssessmentResult : Entity<AssessmentResultId>
{
    private readonly List<TestResult> _testResults = new();
    private readonly List<ExamResult> _examResults = new();
    private readonly List<StudentGrade> _studentGrades = new();

    // Private constructor for controlled creation
    private AssessmentResult(
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

    // Properties with private setters for encapsulation
    public CourseAssessmentId CourseAssessmentId { get; private set; }
    public DepartmentId DepartmentId { get; private set; }
    public int MaxScore { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyList<TestResult> TestResults => _testResults.AsReadOnly();
    public IReadOnlyList<ExamResult> ExamResults => _examResults.AsReadOnly();
    public IReadOnlyList<StudentGrade> StudentGrades => _studentGrades.AsReadOnly();

    // Factory method with validation
    public static Result<AssessmentResult> Create(
        CourseAssessmentId courseAssessmentId,
        DepartmentId departmentId,
        int maxScore,
        string name
    )
    {
        if (courseAssessmentId is null)
        {
            return Result.Failure<AssessmentResult>(
                Error.Validation("CourseAssessmentId.Required", "Course assessment ID is required.")
            );
        }

        if (departmentId is null)
        {
            return Result.Failure<AssessmentResult>(
                Error.Validation("DepartmentId.Required", "Department ID is required.")
            );
        }

        if (maxScore <= 0)
        {
            return Result.Failure<AssessmentResult>(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<AssessmentResult>(
                Error.Validation("Name.Required", "Name is required.")
            );
        }

        var result = new AssessmentResult(
            AssessmentResultId.NewAssessmentResultId(),
            courseAssessmentId,
            departmentId,
            maxScore,
            name
        );
        // Optional: Raise a domain event
        // result.Raise(new AssessmentResultCreatedEvent(result.Id, courseAssessmentId, departmentId, name));
        return Result.Success(result);
    }

    // Update method for basic properties
    public Result UpdateDetails(int maxScore, string name)
    {
        if (maxScore <= 0)
        {
            return Result.Failure(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(Error.Validation("Name.Required", "Name is required."));
        }

        MaxScore = maxScore;
        Name = name;
        // Optional: Raise a domain event
        // Raise(new AssessmentResultUpdatedEvent(Id, CourseAssessmentId, DepartmentId, name));
        return Result.Success();
    }

    public decimal GetTotalScoreForStudent(StudentId studentId)
    {
        if (studentId is null)
        {
            throw new ArgumentNullException(nameof(studentId), "Student ID cannot be null."); // Public method, exception for invalid use
        }

        decimal totalTestScore = _testResults
            .Where(tr => tr.StudentId == studentId)
            .Sum(tr => tr.Score);

        decimal examScore =
            _examResults.FirstOrDefault(er => er.StudentId == studentId)?.Score ?? 0;

        return totalTestScore + examScore;
    }

    public Result AddTestResult(StudentId studentId, TestId testId, decimal score)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (testId is null)
        {
            return Result.Failure(Error.Validation("TestId.Required", "Test ID is required."));
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        TestResult? existingResult = _testResults.FirstOrDefault(tr =>
            tr.StudentId == studentId && tr.TestId == testId
        );
        if (existingResult is not null)
        {
            return Result.Failure(
                Error.Conflict(
                    "TestResult.AlreadyExists",
                    $"Test result for student {studentId} and test {testId} already exists."
                )
            );
        }

        Result<TestResult> testResultResult = TestResult.CreateTestResult(
            testId,
            studentId,
            Id,
            score
        );
        if (testResultResult.IsFailure)
        {
            return testResultResult;
        }

        _testResults.Add(testResultResult.Value);
        // Raise(new TestResultAddedEvent(Id, testResultResult.Value.Id, studentId, score));
        return Result.Success();
    }

    public Result UpdateTestResult(StudentId studentId, TestId testId, decimal score)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (testId is null)
        {
            return Result.Failure(Error.Validation("TestId.Required", "Test ID is required."));
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        TestResult? testResult = _testResults.FirstOrDefault(tr =>
            tr.StudentId == studentId && tr.TestId == testId
        );
        if (testResult == null)
        {
            return Result.Failure(
                Error.NotFound(
                    "TestResult.NotFound",
                    $"Test result for student {studentId} and test {testId} not found."
                )
            );
        }

        testResult.UpdateTestResult(score);
        // Raise(new TestResultUpdatedEvent(Id, testResult.Id, studentId, score));
        return Result.Success();
    }

    public Result RemoveTestResult(StudentId studentId, TestId testId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (testId is null)
        {
            return Result.Failure(Error.Validation("TestId.Required", "Test ID is required."));
        }

        TestResult? testResult = _testResults.FirstOrDefault(tr =>
            tr.StudentId == studentId && tr.TestId == testId
        );
        if (testResult is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "TestResult.NotFound",
                    $"Test result for student {studentId} and test {testId} not found."
                )
            );
        }

        _testResults.Remove(testResult);
        // Raise(new TestResultRemovedEvent(Id, testResult.Id, studentId));
        return Result.Success();
    }

    public Result AddExamResult(StudentId studentId, ExamId examId, decimal score)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (examId is null)
        {
            return Result.Failure(Error.Validation("ExamId.Required", "Exam ID is required."));
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        ExamResult? existingExamResult = _examResults.FirstOrDefault(er =>
            er.StudentId == studentId
        );
        if (existingExamResult != null)
        {
            return Result.Failure(
                Error.Conflict(
                    "ExamResult.AlreadyExists",
                    $"Exam result for student {studentId} already exists."
                )
            );
        }

        Result<ExamResult> examResultResult = ExamResult.CreateExamResult(
            examId,
            studentId,
            Id,
            score
        );
        if (examResultResult.IsFailure)
        {
            return examResultResult;
        }

        _examResults.Add(examResultResult.Value);
        // Raise(new ExamResultAddedEvent(Id, examResultResult.Value.Id, studentId, score));
        return Result.Success();
    }

    public Result UpdateExamResult(StudentId studentId, decimal score)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        ExamResult? examResult = _examResults.FirstOrDefault(er => er.StudentId == studentId);
        if (examResult is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "ExamResult.NotFound",
                    $"Exam result for student {studentId} not found."
                )
            );
        }

        examResult.UpdateExamResult(score);
        // Raise(new ExamResultUpdatedEvent(Id, examResult.Id, studentId, score));
        return Result.Success();
    }

    public Result RemoveExamResult(StudentId studentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        ExamResult? examResult = _examResults.FirstOrDefault(er => er.StudentId == studentId);
        if (examResult is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "ExamResult.NotFound",
                    $"Exam result for student {studentId} not found."
                )
            );
        }

        _examResults.Remove(examResult);
        // Raise(new ExamResultRemovedEvent(Id, examResult.Id, studentId));
        return Result.Success();
    }

    public Result AddStudentGrade(StudentId studentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        StudentGrade? existingGrade = _studentGrades.FirstOrDefault(sg =>
            sg.StudentId == studentId
        );
        if (existingGrade != null)
        {
            return Result.Failure(
                Error.Conflict(
                    "StudentGrade.AlreadyExists",
                    $"Grade for student {studentId} already exists."
                )
            );
        }

        decimal totalScore = GetTotalScoreForStudent(studentId);
        if (totalScore > MaxScore)
        {
            return Result.Failure(
                Error.Validation(
                    "TotalScore.ExceedsMax",
                    $"Total score {totalScore} exceeds max score {MaxScore}."
                )
            );
        }

        Result<StudentGrade> gradeResult = StudentGrade.Create(studentId, Id, totalScore);
        if (gradeResult.IsFailure)
        {
            return gradeResult;
        }

        _studentGrades.Add(gradeResult.Value);
        // Raise(new StudentGradeAddedEvent(Id, gradeResult.Value.Id, studentId, totalScore));
        return Result.Success();
    }

    public Result UpdateStudentGrade(StudentId studentId, decimal totalScore)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (totalScore < 0)
        {
            return Result.Failure(
                Error.Validation("TotalScore.Invalid", "Total score cannot be negative.")
            );
        }

        if (totalScore > MaxScore)
        {
            return Result.Failure(
                Error.Validation(
                    "TotalScore.ExceedsMax",
                    $"Total score {totalScore} exceeds max score {MaxScore}."
                )
            );
        }

        StudentGrade? studentGrade = _studentGrades.FirstOrDefault(sg => sg.StudentId == studentId);
        if (studentGrade is null)
        {
            return Result.Failure(
                Error.NotFound("StudentGrade.NotFound", $"Grade for student {studentId} not found.")
            );
        }

        studentGrade.UpdateTotalScore(totalScore); // Assuming UpdateGrade exists in StudentGrade
        // Raise(new StudentGradeUpdatedEvent(Id, studentGrade.Id, studentId, totalScore));
        return Result.Success();
    }

    public Result RemoveStudentGrade(StudentId studentId)
    {
        if (studentId is null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        StudentGrade? studentGrade = _studentGrades.FirstOrDefault(sg => sg.StudentId == studentId);
        if (studentGrade == null)
        {
            return Result.Failure(
                Error.NotFound("StudentGrade.NotFound", $"Grade for student {studentId} not found.")
            );
        }

        _studentGrades.Remove(studentGrade);
        // Raise(new StudentGradeRemovedEvent(Id, studentGrade.Id, studentId));
        return Result.Success();
    }
}
