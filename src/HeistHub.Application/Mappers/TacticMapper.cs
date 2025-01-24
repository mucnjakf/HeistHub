using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Mappers;

public static class TacticMapper
{
    public static TacticDto ToTacticDto(this Tactic tactic)
    {
        return new TacticDto(tactic.Id, tactic.Name, tactic.Level, tactic.MembersRequired);
    }

    public static HeistTacticDto ToHeistTacticDto(this Tactic tactic)
    {
        return new HeistTacticDto(tactic.Name, tactic.Level, tactic.MembersRequired);
    }

    public static HeistTacticDto ToHeistTacticDto(this HeistTactic heistTactic)
    {
        return new HeistTacticDto(heistTactic.Tactic.Name, heistTactic.Tactic.Level, heistTactic.Tactic.MembersRequired);
    }
}