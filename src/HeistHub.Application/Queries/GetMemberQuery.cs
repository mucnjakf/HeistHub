using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Queries;

public sealed record GetMemberQuery(Guid MemberId) : IRequest<MemberDto>;