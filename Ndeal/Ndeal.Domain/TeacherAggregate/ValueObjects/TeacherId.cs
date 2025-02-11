using Ndeal.SharedKernel;

namespace Ndeal.Domain.TeacherAggregate.ValueObjects;

public class TeacherId : ValueObject
{
    public Guid Value { get; }

    public TeacherId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TeacherId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static TeacherId NewTeacherId()
    {
        return new TeacherId(Guid.NewGuid());
    }

    public static TeacherId FromGuid(Guid guid)
    {
        return new TeacherId(guid);
    }

    public static implicit operator Guid(TeacherId TeacherId)
    {
        return TeacherId.Value;
    }

    public static implicit operator TeacherId(Guid guid)
    {
        return new TeacherId(guid);
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
