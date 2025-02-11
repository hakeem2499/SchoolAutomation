using Ndeal.Domain.TeacherAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.TeacherAggregate.Entities;

public class Qualification : Entity<QualificationId>
{
    public Qualification(
        QualificationId id,
        TeacherId teacherId,
        string name,
        string title,
        DateTime dateObtained,
        string institution
    )
        : base(id)
    {
        TeacherId = teacherId;
        Name = name;
        Title = title;
        DateObtained = dateObtained;
        Institution = institution;
    }

    public TeacherId TeacherId { get; private set; }

    public string Name { get; private set; }
    public string Title { get; private set; }
    public DateTime DateObtained { get; private set; }
    public string Institution { get; private set; }

    internal static Qualification Create(
        TeacherId teacherId,
        string name,
        string title,
        DateTime dateObtained,
        string institution
    )
    {
        var qualification = new Qualification(
            QualificationId.NewQualificationId(),
            teacherId,
            name,
            title,
            dateObtained,
            institution
        );

        return qualification;
    }
}
