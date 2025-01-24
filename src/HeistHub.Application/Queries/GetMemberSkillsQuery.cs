using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Queries;

public sealed record GetMemberSkillsQuery(Guid MemberId) : IRequest<MainMemberSkillDto>;