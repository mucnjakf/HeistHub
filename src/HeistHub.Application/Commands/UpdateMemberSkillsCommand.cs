using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Commands;

// TODO: validation
public record UpdateMemberSkillsCommand(Guid MemberId, IEnumerable<MemberSkillDto> Skills, string MainSkill) : IRequest<Guid>;