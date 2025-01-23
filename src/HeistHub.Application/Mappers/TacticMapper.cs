using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Mappers;

public static class TacticMapper
{
    public static TacticDto ToTacticDto(this Tactic tactic)
    {
        return new TacticDto(tactic.Id, tactic.Name, tactic.Level, tactic.MembersRequired);
    }
}