using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Commands;

public sealed record UpdateHeistTacticsCommand(Guid HeistId, IEnumerable<HeistTacticDto> Tactics) : IRequest;