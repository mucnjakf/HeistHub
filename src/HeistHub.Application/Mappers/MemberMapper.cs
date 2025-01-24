using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Mappers;

public static class MemberMapper
{
    public static MemberDto ToMemberDto(this Member member)
    {
        return new MemberDto(
            member.Name,
            member.Gender,
            member.Email,
            member.MemberSkills?.Select(x => x.Skill.ToMemberSkillDto()) ?? [],
            member.MemberSkills?.First(x => x.IsMain).Skill.Name ?? string.Empty,
            member.Status);
    }
}