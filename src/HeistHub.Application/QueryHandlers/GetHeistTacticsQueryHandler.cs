using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Queries;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using MediatR;

namespace HeistHub.Application.QueryHandlers;

public sealed class GetHeistTacticsQueryHandler(ITacticRepository tacticRepository) 
    : IRequestHandler<GetHeistTacticsQuery, IEnumerable<HeistTacticDto>>
{
    public async Task<IEnumerable<HeistTacticDto>> Handle(GetHeistTacticsQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<HeistTactic> heistTactics = await tacticRepository.GetHeistTacticsAsync(query.HeistId);

        return heistTactics.Select(x => x.ToHeistTacticDto());
    }
}