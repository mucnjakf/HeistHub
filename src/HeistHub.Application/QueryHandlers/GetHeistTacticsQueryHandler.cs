using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Queries;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.QueryHandlers;

public sealed class GetHeistTacticsQueryHandler(ITacticRepository tacticRepository, IHeistRepository heistRepository)
    : IRequestHandler<GetHeistTacticsQuery, IEnumerable<HeistTacticDto>>
{
    public async Task<IEnumerable<HeistTacticDto>> Handle(GetHeistTacticsQuery query, CancellationToken cancellationToken)
    {
        bool heistExists = await heistRepository.ExistsAsync(query.HeistId);

        if (!heistExists)
        {
            throw new HeistNotFoundException($"Heist with ID {query.HeistId} not found.");
        }

        IEnumerable<HeistTactic> heistTactics = await tacticRepository.GetHeistTacticsAsync(query.HeistId);

        return heistTactics.Select(x => x.ToHeistTacticDto());
    }
}