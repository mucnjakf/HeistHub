using HeistHub.Core.Enums;

namespace HeistHub.Application.Dtos;

public sealed record HeistDto(
    string Name,
    string Location,
    DateTimeOffset Start,
    DateTimeOffset End,
    IEnumerable<HeistTacticDto> Tactics,
    HeistStatus Status);