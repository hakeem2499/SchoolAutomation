using Ndeal.Domain.StudentAggregate;
using SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.Entities;

public static class AcademicAggregateErrors
{
    public static Error AlreadyAvailableError(Student student) =>
        Error.Problem("Student.AlreadyRegistered", $"Student {0} is already registered");

    
}
m