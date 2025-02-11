using Ndeal.SharedKernel;

namespace Ndeal.Domain.TeacherAggregate.ValueObjects;

public class QualificationId : ValueObject
{
    public Guid Value { get; }

    public QualificationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("QualificationId cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static QualificationId NewQualificationId()
    {
        return new QualificationId(Guid.NewGuid());
    }

    public static QualificationId FromGuid(Guid guid)
    {
        return new QualificationId(guid);
    }

    public static implicit operator Guid(QualificationId QualificationId)
    {
        return QualificationId.Value;
    }

    public static implicit operator QualificationId(Guid guid)
    {
        return new QualificationId(guid);
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
