using Ndeal.Domain.StudentAggregate.ValueObjects;
using Ndeal.SharedKernel;
using SharedKernel;

namespace Ndeal.Domain.StudentAggregate.Entities;

public class Enrollment : Entity<EnrollmentId>
{
    public Enrollment(
        EnrollmentId id,
        Department department,
        Student student,
        DateTime enrollmentDate
    )
        : base(id)
    {
        Department = department;
        Student = student;
        StudentId = student.Id; // Still store the ID for database purposes
        EnrollmentDate = enrollmentDate;
    }

    public Department Department { get; private set; }
    public Student Student { get; private set; }
    public Guid StudentId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }

    public void ChangeDepartment(Department newDepartment) => Department = newDepartment;
}
