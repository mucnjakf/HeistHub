namespace HeistHub.Application.Dtos;

public sealed record HeistMemberDto(string Name, IEnumerable<MemberSkillDto> Skills);