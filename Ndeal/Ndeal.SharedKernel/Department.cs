using SharedKernel;

namespace Ndeal.SharedKernel;

public class Department : Entity
{
    public Department(Guid id, string name)
        : base(id) => Name = name;

    public string Name { get; private set; }
}
