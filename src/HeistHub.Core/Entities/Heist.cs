using HeistHub.Core.Enums;

namespace HeistHub.Core.Entities;

public sealed class Heist : Entity
{
    public string Name { get; private set; } // TODO unique

    public string Location { get; private set; }

    public DateTimeOffset Start { get; private set; }

    public DateTimeOffset End { get; private set; }

    public HeistStatus Status { get; private set; } = HeistStatus.Planning;

    public bool IsSuccess { get; private set; }

    public IEnumerable<HeistSkill> RequiredSkills { get; private set; }

    public IEnumerable<Member>? Members { get; private set; }

    private Heist(
        Guid id,
        string name,
        string location,
        DateTimeOffset start,
        DateTimeOffset end,
        IEnumerable<HeistSkill> requiredSkills) : base(id)
    {
        Name = name;
        Location = location;
        Start = start;
        End = end;
        RequiredSkills = requiredSkills;
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