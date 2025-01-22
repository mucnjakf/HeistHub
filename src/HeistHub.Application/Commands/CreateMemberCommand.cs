using HeistHub.Core.Enums;
using MediatR;

namespace HeistHub.Application.Commands;

public sealed record CreateMemberCommand(
    string Name,
    Gender Gender,
    string Email,
    IEnumerable<CreateMemberSkillCommand> Skills,
    string MainSkill,
    MemberStatus Status) : IRequest<Guid>;