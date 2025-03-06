using Ndeal.Domain.AcademicAggregate.Entities;
using Ndeal.Domain.AcademicAggregate.Enums;
using Ndeal.Domain.AcademicAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.AcademicAggregate;

public class AcademicSession(
    AcademicSessionId id,
    string name,
    MonthYear startDate,
    MonthYear endDate
) : AggregateRoot<AcademicSessionId>(id)
{
    private readonly List<Semester> _semesters = new();

    public string Name { get; private set; } = name;
    public MonthYear StartDate { get; private set; } = startDate;
    public MonthYear EndDate { get; private set; } = endDate;
    public IReadOnlyCollection<Semester> Semesters => _semesters.AsReadOnly();

    public static Result CreateAcademicSession(string name, MonthYear startDate, MonthYear endDate)
    {
        Result<AcademicSession> result = new AcademicSession(
            AcademicSessionId.NewAcademicSessionId(),
            name,
            startDate,
            endDate
        );
        return result;
    }

    public Result AddSemester(SemesterType name, DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return Result.Failure<Semester>(
                Error.Conflict("Semester.InvalidDate", "StartDate cannot be after end date")
            );
        }

        Result<Semester> semester = Semester.Create(Id, name, startDate, endDate);
        _semesters.Add(semester.Value);
        return Result.Success();
    }

    public Result UpdateSemester(
        SemesterId semesterId,
        SemesterType name,
        DateTime startDate,
        DateTime endDate
    )
    {
        Semester? semester = _semesters.FirstOrDefault(s => s.Id == semesterId);
        if (startDate > endDate)
        {
            return Result.Failure<Semester>(
                Error.Conflict("Semester.InvalidDate", "StartDate cannot be after end date")
            );
        }

        semester?.UpdateSemester(name, startDate, endDate);
        return Result.Success();
    }
}
