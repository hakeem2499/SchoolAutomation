using Ndeal.SharedKernel;

namespace SharedKernel;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(TId id) => Id = id;

    public TId Id { get; protected set; } // Or just 'get;' if your ORM handles ID assignment

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    // internal for same-assembly access, or leave protected
    protected void Raise(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
        {
            throw new ArgumentNullException(nameof(domainEvent));
        }
        _domainEvents.Add(domainEvent);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> entity)
        {
            return false; // Handle the case where the cast fails
        }
        return Id.Equals(entity.Id); // Or add other properties to the comparison if needed
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

    public override int GetHashCode() => HashCode.Combine(Id, GetType());

    public bool Equals(Entity<TId>? other) => Equals((object?)other);
}