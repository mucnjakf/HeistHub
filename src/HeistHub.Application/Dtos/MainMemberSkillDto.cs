namespace HeistHub.Application.Dtos;

public sealed record MainMemberSkillDto(IEnumerable<MemberSkillDto> Skills, string MainSkill);