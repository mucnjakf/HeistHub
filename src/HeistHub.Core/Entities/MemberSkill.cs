namespace HeistHub.Core.Entities;

public sealed class MemberSkill
{
    public Guid MemberId { get; init; }
    
    public Member Member { get; private set; } = null!;

    public Guid SkillId { get; init; }
    
    public Skill Skill { get; private set; } = null!;

    public bool IsMain { get; init; }
}