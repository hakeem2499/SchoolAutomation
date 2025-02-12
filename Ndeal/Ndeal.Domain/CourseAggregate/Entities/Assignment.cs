using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate.Entities;

public class Assignment : Entity<AssignmentId>
{
    private readonly List<AssignmentScore> _assignmentScores = new();

    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Course Course { get; private set; }
    public CourseId CourseId => Course.Id;
    public TeacherId TeacherId { get; private set; }
    public int MaxScore { get; private set; }

    public IReadOnlyList<AssignmentScore> AssignmentScores => _assignmentScores.AsReadOnly();

    // Private constructor (for Entity Framework or other ORMs)
    private Assignment(
        AssignmentId id,
        string title,
        string description,
        DateTime dueDate,
        DateTime createdAt,
        Course course,
        TeacherId teacherId,
        int maxScore
    )
        : base(id)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        CreatedAt = createdAt;
        Course = course;
        TeacherId = teacherId;
        MaxScore = maxScore;
    }

    // Factory method for creating new Assignments (recommended approach)
    internal static Assignment Create(
        string title,
        string description,
        DateTime dueDate,
        Course course,
        TeacherId teacherId,
        int maxScore,
        DateTime? createdAt = null // Optional createdAt
    )
    {
        return new Assignment(
            AssignmentId.NewAssignmentId(),
            title,
            description,
            dueDate,
            createdAt ?? DateTime.UtcNow, // Use UTC now if not provided
            course,
            teacherId,
            maxScore
        );
    }

    public void AddAssignmentScore(StudentId studentId, int score)
    {
        // Check if a score for this student already exists before adding.
        if (_assignmentScores.Any(x => x.StudentId == studentId))
        {
            throw new InvalidOperationException(
                $"Score for student {studentId} already exists for this assignment."
            );
        }

        var assignmentScore = AssignmentScore.Create(this, studentId, score);
        _assignmentScores.Add(assignmentScore);

        //Consider raising a domain event here.
    }

    public void UpdateAssignmentScore(StudentId studentId, int score)
    {
        AssignmentScore? assignmentScore =
            _assignmentScores.FirstOrDefault(x => x.StudentId == studentId)
            ?? throw new InvalidOperationException(
                $"Score for student {studentId} not found for this assignment."
            );

        assignmentScore.UpdateScore(score);
        // raise a domain event here.
    }

    public void RemoveAssignmentScore(StudentId studentId)
    {
        AssignmentScore? assignmentScore =
            _assignmentScores.FirstOrDefault(x => x.StudentId == studentId)
            ?? throw new InvalidOperationException(
                $"Score for student {studentId} not found for this assignment."
            );

        _assignmentScores.Remove(assignmentScore);
        // raise a domain event here.
    }
}
