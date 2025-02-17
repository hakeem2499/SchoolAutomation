using SharedKernel;

namespace Ndeal.Domain.ResourceAggregate.Entities;

public static class ResourceErrors
{
    public static Error ResourceUnavailable(Resource resource) =>
        Error.Problem("Resource.Unavailable", $"Resource {resource.Name} is not available");
}
