namespace HeistHub.Core.Entities;

public abstract class Entity(Guid id)
{
    public Guid Id { get; private set; } = id;
}