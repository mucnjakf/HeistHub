using HeistHub.Application.Dtos;
using HeistHub.Application.Queries;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using MediatR;

namespace HeistHub.Application.QueryHandlers;

public sealed class GetHeistStatusQueryHandler(IHeistRepository heistRepository) : IRequestHandler<GetHeistStatusQuery, HeistStatusDto>
{
    public async Task<HeistStatusDto> Handle(GetHeistStatusQuery query, CancellationToken cancellationToken)
    {
        Heist heist = await heistRepository.GetAsync(query.HeistId);

        return new HeistStatusDto(heist.Status);
    }
}