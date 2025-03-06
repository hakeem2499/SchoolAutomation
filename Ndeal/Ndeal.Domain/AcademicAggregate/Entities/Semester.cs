using Ndeal.Domain.AcademicAggregate.Enums;
using Ndeal.Domain.AcademicAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.Entities;

public sealed class Semester : Entity<SemesterId>
{
    internal Semester(
        SemesterId semesterId,
        AcademicSessionId academicSessionId,
        SemesterType name,
        DateTime startDate,
        DateTime endDate
    )
        : base(semesterId)
    {
        AcademicSessionId = academicSessionId;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }

    public AcademicSessionId AcademicSessionId { get; private set; }
    public SemesterType Name { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    internal static Result<Semester> Create(
        AcademicSessionId academicSessionId,
        SemesterType name,
        DateTime startDate,
        DateTime endDate
    )
    {
        return new Semester(
            SemesterId.NewSemesterId(),
            academicSessionId,
            name,
            startDate,
            endDate
        );
    }

    internal void UpdateSemester(SemesterType name, DateTime startDate, DateTime endDate)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }
}
