using Ndeal.Domain.StudentAggregate.ValueObjects;
using SharedKernel;

namespace Ndeal.Domain.StudentAggregate.Entities;

public class Guardian : Entity<GuardianId>
{
    public Guardian(GuardianId id, string name, string phoneNumber, string email)
        : base(id)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public string Name { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
}
