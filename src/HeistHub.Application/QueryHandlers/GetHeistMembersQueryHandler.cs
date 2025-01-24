using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Queries;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Core.Enums;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.QueryHandlers;

public sealed class GetHeistMembersQueryHandler(IHeistRepository heistRepository)
    : IRequestHandler<GetHeistMembersQuery, IEnumerable<HeistMemberDto>>
{
    public async Task<IEnumerable<HeistMemberDto>> Handle(GetHeistMembersQuery query, CancellationToken cancellationToken)
    {
        Heist heist = await heistRepository.GetAsync(query.HeistId);

        if (heist.Status is HeistStatus.Planning)
        {
            throw new HeistStatusException("Cannot get members from a planning heist.");
        }

        return heist.Members?.Select(x => x.ToHeistMemberDto()) ?? [];
    }
}