namespace Livetta.Domain.Contracts;

public interface IEntity
{
    Guid Id { get; }
}

public abstract class Entity(Guid id) : IEntity
{
    protected Entity() : this(Guid.NewGuid())
    {
    }

    public Guid Id { get; set; } = id;
}