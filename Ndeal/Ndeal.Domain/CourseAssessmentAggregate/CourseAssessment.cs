using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.Entities;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate;

public class CourseAssessment : AggregateRoot<CourseAssessmentId>
{
    private readonly List<Test> _tests = new();

    // Private constructor for controlled creation
    private CourseAssessment(
        CourseAssessmentId id,
        CourseId courseId,
        SemesterId semesterId,
        string name
    )
        : base(id)
    {
        CourseId = courseId;
        SemesterId = semesterId;
        Name = name;
    }

    // Properties with private setters for encapsulation
    public CourseId CourseId { get; private set; }
    public SemesterId SemesterId { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyList<Test> Tests => _tests.AsReadOnly();
    public Exam? Exam { get; private set; }
    public AssessmentResult? AssessmentResult { get; private set; }

    // Factory method with validation
    public static Result<CourseAssessment> Create(
        CourseId courseId,
        SemesterId semesterId,
        string name
    )
    {
        if (courseId == null)
        {
            return Result.Failure<CourseAssessment>(
                Error.Validation("CourseId.Required", "Course ID is required.")
            );
        }

        if (semesterId == null)
        {
            return Result.Failure<CourseAssessment>(
                Error.Validation("SemesterId.Required", "Semester ID is required.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<CourseAssessment>(
                Error.Validation("Name.Required", "Assessment name is required.")
            );
        }

        var assessment = new CourseAssessment(
            CourseAssessmentId.NewCourseAssessmentId(),
            courseId,
            semesterId,
            name
        );
        // Optional: Raise a domain event
        // assessment.Raise(new CourseAssessmentCreatedEvent(assessment.Id, courseId, semesterId, name));
        return Result.Success(assessment);
    }

    // Update method for basic properties
    public Result UpdateDetails(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(
                Error.Validation("Name.Required", "Assessment name is required.")
            );
        }

        Name = name;
        // Optional: Raise a domain event
        // Raise(new CourseAssessmentUpdatedEvent(Id, CourseId, SemesterId, name));
        return Result.Success();
    }

    public Result AddTest(int resultWeight, int maxScore, string name)
    {
        if (resultWeight <= 0)
        {
            return Result.Failure(
                Error.Validation("ResultWeight.Invalid", "Result weight must be greater than zero.")
            );
        }

        if (maxScore <= 0)
        {
            return Result.Failure(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(Error.Validation("Name.Required", "Test name is required."));
        }

        Result<Test> testResult = Test.CreateTest(Id, resultWeight, maxScore, name);
        if (testResult.IsFailure)
        {
            return testResult;
        }

        _tests.Add(testResult.Value);
        // Raise(new TestAddedEvent(Id, testResult.Value.Id, name));
        return Result.Success();
    }

    public Result UpdateTest(TestId testId, int resultWeight, int maxScore, string name)
    {
        if (testId == null)
        {
            return Result.Failure(Error.Validation("TestId.Required", "Test ID is required."));
        }

        if (resultWeight <= 0)
        {
            return Result.Failure(
                Error.Validation("ResultWeight.Invalid", "Result weight must be greater than zero.")
            );
        }

        if (maxScore <= 0)
        {
            return Result.Failure(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(Error.Validation("Name.Required", "Test name is required."));
        }

        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null)
        {
            return Result.Failure(Error.NotFound("Test.NotFound", $"Test {testId} not found."));
        }

        test.UpdateTest(resultWeight, maxScore, name);
        // Raise(new TestUpdatedEvent(Id, testId, name));
        return Result.Success();
    }

    public Result RemoveTest(TestId testId)
    {
        if (testId == null)
        {
            return Result.Failure(Error.Validation("TestId.Required", "Test ID is required."));
        }

        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null)
        {
            return Result.Failure(Error.NotFound("Test.NotFound", $"Test {testId} not found."));
        }

        _tests.Remove(test);
        // Raise(new TestRemovedEvent(Id, testId));
        return Result.Success();
    }

    public Result AddExam(int resultWeight, int maxScore, string name)
    {
        if (Exam != null)
        {
            return Result.Failure(
                Error.Conflict(
                    "Exam.AlreadyExists",
                    "An exam is already assigned to this assessment."
                )
            );
        }

        if (resultWeight <= 0)
        {
            return Result.Failure(
                Error.Validation("ResultWeight.Invalid", "Result weight must be greater than zero.")
            );
        }

        if (maxScore <= 0)
        {
            return Result.Failure(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(Error.Validation("Name.Required", "Exam name is required."));
        }

        var examResult = Exam.CreateExam(Id, resultWeight, maxScore, name);
        if (examResult.IsFailure)
        {
            return examResult;
        }

        Exam = examResult.Value;
        // Raise(new ExamAddedEvent(Id, examResult.Value.Id, name));
        return Result.Success();
    }

    public Result UpdateExam(int resultWeight, int maxScore, string name)
    {
        if (Exam == null)
        {
            return Result.Failure(
                Error.NotFound("Exam.NotFound", "No exam exists for this assessment.")
            );
        }

        if (resultWeight <= 0)
        {
            return Result.Failure(
                Error.Validation("ResultWeight.Invalid", "Result weight must be greater than zero.")
            );
        }

        if (maxScore <= 0)
        {
            return Result.Failure(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(Error.Validation("Name.Required", "Exam name is required."));
        }

        Exam.UpdateExam(resultWeight, maxScore, name);
        // Raise(new ExamUpdatedEvent(Id, Exam.Id, name));
        return Result.Success();
    }

    public Result RemoveExam()
    {
        if (Exam == null)
        {
            return Result.Failure(Error.NotFound("Exam.NotFound", "No exam exists to remove."));
        }

        Exam = null;
        // Raise(new ExamRemovedEvent(Id));
        return Result.Success();
    }

    public Result AddAssessmentResult(DepartmentId departmentId, int maxScore, string name)
    {
        if (AssessmentResult != null)
        {
            return Result.Failure(
                Error.Conflict(
                    "AssessmentResult.AlreadyExists",
                    "An assessment result already exists."
                )
            );
        }

        if (departmentId == null)
        {
            return Result.Failure(
                Error.Validation("DepartmentId.Required", "Department ID is required.")
            );
        }

        if (maxScore <= 0)
        {
            return Result.Failure(
                Error.Validation("MaxScore.Invalid", "Max score must be greater than zero.")
            );
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(
                Error.Validation("Name.Required", "Assessment result name is required.")
            );
        }

        var result = AssessmentResult.CreateAssessmentResult(Id, departmentId, maxScore, name);
        if (result.IsFailure)
        {
            return result;
        }

        AssessmentResult = result.Value;
        // Raise(new AssessmentResultAddedEvent(Id, departmentId, name));
        return Result.Success();
    }

    public Result AddTestResult(StudentId studentId, TestId testId, decimal score)
    {
        if (studentId == null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (testId == null)
        {
            return Result.Failure(Error.Validation("TestId.Required", "Test ID is required."));
        }

        if (AssessmentResult == null)
        {
            return Result.Failure(
                Error.NotFound("AssessmentResult.NotFound", "No assessment result exists.")
            );
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        var test = _tests.FirstOrDefault(t => t.Id == testId);
        if (test == null)
        {
            return Result.Failure(Error.NotFound("Test.NotFound", $"Test {testId} not found."));
        }

        if (score > test.MaxScore)
        {
            return Result.Failure(
                Error.Validation(
                    "Score.ExceedsMax",
                    $"Score {score} exceeds max score {test.MaxScore}."
                )
            );
        }

        decimal testMarkObtained = score / test.MaxScore * test.ResultWeight;
        AssessmentResult.AddTestResult(studentId, testId, testMarkObtained);
        // Raise(new TestResultAddedEvent(Id, studentId, testId, testMarkObtained));
        return Result.Success();
    }

    public Result AddExamResult(StudentId studentId, decimal score)
    {
        if (studentId == null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (AssessmentResult == null)
        {
            return Result.Failure(
                Error.NotFound("AssessmentResult.NotFound", "No assessment result exists.")
            );
        }

        if (Exam == null)
        {
            return Result.Failure(Error.NotFound("Exam.NotFound", "No exam exists."));
        }

        if (score < 0)
        {
            return Result.Failure(Error.Validation("Score.Invalid", "Score cannot be negative."));
        }

        if (score > Exam.MaxScore)
        {
            return Result.Failure(
                Error.Validation(
                    "Score.ExceedsMax",
                    $"Score {score} exceeds max score {Exam.MaxScore}."
                )
            );
        }

        decimal examMarkObtained = score / Exam.MaxScore * Exam.ResultWeight;
        AssessmentResult.AddExamResult(studentId, Exam.Id, examMarkObtained);
        // Raise(new ExamResultAddedEvent(Id, studentId, Exam.Id, examMarkObtained));
        return Result.Success();
    }

    public Result AddStudentGrade(StudentId studentId)
    {
        if (studentId == null)
        {
            return Result.Failure(
                Error.Validation("StudentId.Required", "Student ID is required.")
            );
        }

        if (AssessmentResult == null)
        {
            return Result.Failure(
                Error.NotFound("AssessmentResult.NotFound", "No assessment result exists.")
            );
        }

        AssessmentResult.AddStudentGrade(studentId);
        // Raise(new StudentGradeAddedEvent(Id, studentId));
        return Result.Success();
    }
}
