namespace HeistHub.Core.Entities;

public sealed class MemberSkill : Skill
{
    public bool IsMain { get; private set; }

    private MemberSkill(Guid id, string name, string level, bool isMain) : base(id, name, level)
    {
        IsMain = isMain;
    }

    public static MemberSkill Create(string name, string level, bool isMain)
    {
        Guid id = Guid.NewGuid();

        return new MemberSkill(id, name, level, isMain);
    }
}