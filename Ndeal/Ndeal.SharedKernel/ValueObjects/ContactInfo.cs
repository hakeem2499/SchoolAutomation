using Ndeal.SharedKernel;

namespace Ndeal.SharedKernel.ValueObjects;

public class ContactInfo : ValueObject
{
    public string PhoneNumber { get; }
    public string Email { get; }

    public ContactInfo(string phoneNumber, string email)
    {
        PhoneNumber = phoneNumber;
        Email = email;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PhoneNumber;
        yield return Email;
    }
}
