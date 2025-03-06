using Ndeal.Domain.StudentAggregate;
using SharedKernel;

namespace Ndeal.Domain.AcademicAggregate.Entities;

public static class AcademicAggregateErrors
{
    public static Error AlreadyAvailableError(Student student) =>
        Error.Problem(
            "Student.AlreadyRegistered",
            $"Student {student.LastName}, {student.FirstName} is already registered"
        );
}
