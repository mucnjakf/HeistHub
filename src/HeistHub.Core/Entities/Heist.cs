using HeistHub.Core.Enums;

namespace HeistHub.Core.Entities;

public sealed class Heist
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!; // TODO unique

    public string Location { get; private set; } = null!;

    public DateTimeOffset Start { get; private set; }

    public DateTimeOffset End { get; private set; }

    public HeistStatus Status { get; private set; }

    public bool IsSuccess { get; private set; }

    public IEnumerable<HeistSkill> RequiredSkills { get; private set; } = null!;

    public IEnumerable<Member>? Members { get; private set; }

    private Heist()
    {
    }

    private Heist(
        Guid id,
        string name,
        string location,
        DateTimeOffset start,
        DateTimeOffset end,
        IEnumerable<HeistSkill> requiredSkills)
    {
        Id = id;
        Name = name;
        Location = location;
        Start = start;
        End = end;
        RequiredSkills = requiredSkills;
        Status = HeistStatus.Planning;
    }

    public static Heist Create(
        string name,
        string location,
        DateTimeOffset start,
        DateTimeOffset end,
        IEnumerable<HeistSkill> requiredSkills)
    {
        Guid id = Guid.NewGuid();

        return new Heist(id, name, location, start, end, requiredSkills);
    }
}