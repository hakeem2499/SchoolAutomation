using Ndeal.SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate.ValueObjects;

public class DepartmentId : ValueObject
{
    public Guid Value { get; }

    public DepartmentId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("DepartmentId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static DepartmentId NewDepartmentId()
    {
        return new DepartmentId(Guid.NewGuid());
    }

    public static DepartmentId FromGuid(Guid guid)
    {
        return new DepartmentId(guid);
    }

    public static implicit operator Guid(DepartmentId DepartmentId)
    {
        return DepartmentId.Value;
    }

    public static implicit operator DepartmentId(Guid guid)
    {
        return new DepartmentId(guid);
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
