namespace HeistHub.Core.Entities;

public sealed class HeistSkill : Skill
{
    public int MembersRequired { get; private set; }
    
    private HeistSkill(Guid id, string name, string level, int membersRequired) : base(id, name, level)
    {
        MembersRequired = membersRequired;
    }

    public static HeistSkill Create(string name, string level, int membersRequired)
    {
        Guid id = Guid.NewGuid();

        return new HeistSkill(id, name, level, membersRequired);
    }
}