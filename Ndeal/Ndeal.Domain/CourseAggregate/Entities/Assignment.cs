using Ndeal.Domain.CourseAggregate.ValueObjects;
using Ndeal.Domain.TeacherAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAggregate.Entities;

public class Assignment : Entity<AssignmentId>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public CourseId CourseId { get; private set; }

    public TeacherId TeacherId { get; private set; }

    public int MaxScore { get; private set; }

    public Assignment(
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

    internal static Assignment Create(
        string title,
        string description,
        DateTime dueDate,
        DateTime createdAt,
        CourseId courseId,
        TeacherId teacherId,
        int MaxScore
    )
    {
        return new Assignment(
            AssignmentId.NewAssignmentId(),
            title,
            description,
            dueDate,
            createdAt,
            courseId,
            teacherId,
            MaxScore
        );
    }
}
