using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Queries;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using MediatR;

namespace HeistHub.Application.QueryHandlers;

public sealed class GetHeistQueryHandler(IHeistRepository heistRepository) : IRequestHandler<GetHeistQuery, HeistDto>
{
    public async Task<HeistDto> Handle(GetHeistQuery query, CancellationToken cancellationToken)
    {
        Heist heist = await heistRepository.GetAsync(query.HeistId);

        return heist.ToHeistDto();
    }
}