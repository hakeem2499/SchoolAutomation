using Ndeal.Domain.AcademicAggregate.Enums;
using Ndeal.Domain.AcademicAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.Entities;

public class Semester : Entity<SemesterId>
{
    public Semester(
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

    internal static Semester Create(
        AcademicSessionId academicSessionId,
        SemesterType name,
        DateTime startDate,
        DateTime endDate
    )
    {
        // Add validation here (e.g., StartDate before EndDate, valid AcademicSessionId, etc.)
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be after end date.");
        }

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
        // Add validation here (e.g., StartDate before EndDate, valid AcademicSessionId, etc.)
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be after end date.");
        }

        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }
}
