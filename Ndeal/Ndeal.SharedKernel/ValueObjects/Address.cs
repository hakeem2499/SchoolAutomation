using Ndeal.SharedKernel;

namespace Ndeal.SharedKernel.ValueObjects;

public class Address : ValueObject
{
    public string StreetAddress { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; } // Optional

    public Address(string streetAddress, string city, string state, string zipCode, string? country)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country ?? "Nigeria";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StreetAddress;
        yield return City;
        yield return State;
        yield return ZipCode;
        yield return Country;
    }
}
