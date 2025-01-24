using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Mappers;

public static class SkillMapper
{
    public static SkillDto ToSkillDto(this Skill skill)
    {
        return new SkillDto(skill.Id, skill.Name, skill.Level);
    }

    public static MemberSkillDto ToMemberSkillDto(this Skill skill)
    {
        return new MemberSkillDto(skill.Name, skill.Level);
    }

    public static MemberSkillDto ToMemberSkillDto(this MemberSkill memberSkill)
    {
        return new MemberSkillDto(memberSkill.Skill.Name, memberSkill.Skill.Level);
    }
}