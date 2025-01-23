using HeistHub.Application.Dtos;

namespace HeistHub.Application.Requests;

public sealed record UpdateHeistTacticsRequest(IEnumerable<HeistTacticDto> Tactics);