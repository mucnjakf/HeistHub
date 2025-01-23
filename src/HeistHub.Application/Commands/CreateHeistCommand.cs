using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Commands;

public sealed record CreateHeistCommand(
    string Name,
    string Location,
    DateTimeOffset Start,
    DateTimeOffset End,
    IEnumerable<HeistTacticDto> Tactics) : IRequest<Guid>;