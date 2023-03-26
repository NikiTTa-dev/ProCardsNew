namespace ProCardsNew.Domain.Common.Models;

public abstract class Entity<TId> : Entity, IEquatable<Entity<TId>>
    where TId: notnull
{
    public TId Id { get; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Entity<TId>)
            return false;

        var entity = (Entity<TId>)obj;

        return GetEqualityComponents()
            .SequenceEqual(entity.GetEqualityComponents());
    }
    
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }
    
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?) other);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }
    
#pragma warning disable CS8618
    protected Entity()
    {
    }
#pragma warning restore CS8618
}