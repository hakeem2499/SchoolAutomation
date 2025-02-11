using Ndeal.SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate.ValueObjects;

public class TeacherAssignmentId : ValueObject
{
    public Guid Value { get; }

    public TeacherAssignmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TeacherAssignmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static TeacherAssignmentId NewTeacherAssignmentId()
    {
        return new TeacherAssignmentId(Guid.NewGuid());
    }

    public static TeacherAssignmentId FromGuid(Guid guid)
    {
        return new TeacherAssignmentId(guid);
    }

    public static implicit operator Guid(TeacherAssignmentId TeacherAssignmentId)
    {
        return TeacherAssignmentId.Value;
    }

    public static implicit operator TeacherAssignmentId(Guid guid)
    {
        return new TeacherAssignmentId(guid);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
