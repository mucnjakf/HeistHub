using MediatR;

namespace HeistHub.Application.Commands;

public sealed record DeleteMemberSkillCommand(Guid MemberId, string SkillName) : IRequest;