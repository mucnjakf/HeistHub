namespace HeistHub.Core.Entities;

public sealed class Tactic
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public int MembersRequired { get; private set; }

    public string Level { get; private set; } = null!;

    public IEnumerable<HeistTactic>? HeistTactics { get; private set; }

    public Tactic()
    {
    }

    private Tactic(Guid id, string name, string level, int membersRequired)
    {
        Id = id;
        Name = name;
        Level = level;
        MembersRequired = membersRequired;
    }

    public static Tactic Create(string name, int membersRequired, string level)
    {
        Guid id = Guid.NewGuid();

        return new Tactic(id, name, level, membersRequired);
    }
}