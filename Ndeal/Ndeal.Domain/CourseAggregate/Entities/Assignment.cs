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
    public CourseId CourseId { get; private set; }

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
        CourseId courseId,
        TeacherId teacherId,
        int maxScore
    )
        : base(id)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        CreatedAt = createdAt;
        CourseId = courseId;
        TeacherId = teacherId;
        MaxScore = maxScore;
    }

    // Factory method for creating new Assignments (recommended approach)
    internal static Assignment Create(
        string title,
        string description,
        DateTime dueDate,
        CourseId courseId,
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
            courseId,
            teacherId,
            maxScore
        );
    }

    internal AssignmentScore AddAssignmentScore(AssignmentScore assignmentScore)
    {
        _assignmentScores.Add(assignmentScore);
        return assignmentScore;
    }

    internal AssignmentScore RemoveAssignmentScore(AssignmentScore assignmentScore)
    {
        _assignmentScores.Remove(assignmentScore);
        return assignmentScore;
    }
}
