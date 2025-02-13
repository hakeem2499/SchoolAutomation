using Ndeal.Domain.AssessmentAggregate.ValueObjects;
using Ndeal.Domain.CourseAssessmentAggregate.ValueObjects;
using Ndeal.Domain.DepartmentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.CourseAssessmentAggregate.Entities;

public class AssessmentResult : Entity<AssessmentResultId>
{
    
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
}
