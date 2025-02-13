using Ndeal.Domain.AcademicAggregate.Entities;
using Ndeal.Domain.AcademicAggregate.Enums;
using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.AcademicAggregate;

public class AcademicSession : AggregateRoot<AcademicSessionId>
{
    private readonly List<Semester> semesters = new();

    public AcademicSession(
        AcademicSessionId id,
        string name,
        MonthYear startDate,
        MonthYear endDate
    )
        : base(id)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Name { get; private set; }
    public MonthYear StartDate { get; private set; }
    public MonthYear EndDate { get; private set; }
    public IReadOnlyList<Semester> Semesters => semesters.AsReadOnly();

    public static AcademicSession CreateAcademicSession(
        string name,
        MonthYear startDate,
        MonthYear endDate
    )
    {
        return new AcademicSession(
            AcademicSessionId.NewAcademicSessionId(),
            name,
            startDate,
            endDate
        );
    }

    public void AddSemester(SemesterType name, DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be after end date.");
        }

        var semester = Semester.Create(Id, name, startDate, endDate);
        semesters.Add(semester);
    }

    public void UpdateSemester(
        SemesterId semesterId,
        SemesterType name,
        DateTime startDate,
        DateTime endDate
    )
    {
        Semester? semester = semesters.FirstOrDefault(s => s.Id == semesterId);
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be after end date.");
        }

        semester.UpdateSemester(name, startDate, endDate);
    }
}
