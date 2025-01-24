using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Mappers;

public static class HeistMapper
{
    public static HeistDto ToHeistDto(this Heist heist)
    {
        return new HeistDto(
            heist.Name,
            heist.Location,
            heist.Start,
            heist.End,
            heist.HeistTactics?.Select(x => x.Tactic.ToHeistTacticDto()) ?? [],
            heist.Status);
    }
}