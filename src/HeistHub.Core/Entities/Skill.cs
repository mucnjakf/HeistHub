namespace HeistHub.Core.Entities;

public abstract class Skill(Guid id, string name, string level = "*")
{
    public Guid Id { get; private set; } = id;

    public string Name { get; private set; } = name; // TODO: unique

    public string Level { get; private set; } = level; // TODO: default "*" max 10
}