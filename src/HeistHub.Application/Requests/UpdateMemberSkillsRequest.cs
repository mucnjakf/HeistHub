using HeistHub.Application.Dtos;

namespace HeistHub.Application.Requests;

public record UpdateMemberSkillsRequest(IEnumerable<MemberSkillDto> Skills, string MainSkill);