using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Queries;

public sealed record GetHeistTacticsQuery(Guid HeistId) : IRequest<IEnumerable<HeistTacticDto>>;