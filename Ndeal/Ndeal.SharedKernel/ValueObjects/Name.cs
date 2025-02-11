using Ndeal.SharedKernel;

namespace Ndeal.SharedKernel.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; }
    public string MiddleName { get; } // Optional
    public string LastName { get; }

    public Name(string firstName, string lastName, string? middleName = null)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName ?? string.Empty; // Handle null middle names
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return MiddleName;
        yield return LastName;
    }

    // ... (Optional: Add methods for formatting the name, e.g., GetFullName())
}
