using HeistHub.Core.Enums;

namespace HeistHub.Application.Dtos;

public sealed record MemberDto(string Name, Gender Gender, string Email, IEnumerable<MemberSkillDto> Skills, string MainSkill, MemberStatus Status);