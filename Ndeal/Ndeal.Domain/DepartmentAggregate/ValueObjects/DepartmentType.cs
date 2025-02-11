using Ndeal.SharedKernel;

namespace Ndeal.Domain.DepartmentAggregate;

public class DepartmentType : ValueObject
{
    public string ProgramName { get; }
    public Level Level { get; }

    public DepartmentType(string programName, Level level)
    {
        if (string.IsNullOrWhiteSpace(programName))
        {
            throw new ArgumentException("ProgramName cannot be empty", nameof(programName));
        }
        ProgramName = programName;
        Level = level;
    }

    public override string ToString()
    {
        return $"{ProgramName} - {Level}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProgramName;
        yield return Level;
    }
}
