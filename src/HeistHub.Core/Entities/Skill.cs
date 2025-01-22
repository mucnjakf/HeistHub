namespace HeistHub.Core.Entities;

public sealed class Skill
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string Level { get; private set; } = null!;

    public IEnumerable<MemberSkill>? MemberSkills { get; private set; }

    private Skill()
    {
    }

    private Skill(Guid id, string name, string level)
    {
        Id = id;
        Name = name;
        Level = level;
    }

    public static Skill Create(string name, string level) 
        => new(Guid.NewGuid(), name, level);
}