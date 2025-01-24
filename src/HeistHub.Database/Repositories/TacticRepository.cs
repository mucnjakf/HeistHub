using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace HeistHub.Database.Repositories;

public class TacticRepository(ApplicationDbContext applicationDbContext) : ITacticRepository
{
    public async Task<IEnumerable<TacticDto>> GetAllByNameLevelAndMembersRequiredAsync(List<HeistTacticDto> tactics)
    {
        List<Tactic> dbTactics = await applicationDbContext.Tactics.ToListAsync();

        return dbTactics
            .Where(x => tactics.Any(y => y.Name == x.Name && y.Level == x.Level && y.MembersRequired == x.MembersRequired))
            .Select(x => x.ToTacticDto());
    }

    public async Task<IEnumerable<TacticDto>> CreateAsync(IEnumerable<HeistTacticDto> tactics)
    {
        List<Tactic> newTactics = [];

        newTactics.AddRange(tactics.Select(x => Tactic.Create(x.Name, x.MembersRequired, x.Level)));

        await applicationDbContext.Tactics.AddRangeAsync(newTactics);
        await applicationDbContext.SaveChangesAsync();

        return newTactics.Select(x => x.ToTacticDto());
    }

    public async Task CreateHeistTacticsAsync(Guid heistId, IEnumerable<Guid> tacticIds)
    {
        IEnumerable<HeistTactic> heistTactics = tacticIds
            .Select(tacticId => new HeistTactic { HeistId = heistId, TacticId = tacticId });

        await applicationDbContext.HeistTactics
            .AddRangeAsync(heistTactics);

        await applicationDbContext.SaveChangesAsync();
    }

    public async Task RemoveHeistTacticsAsync(Guid heistId)
    {
        List<HeistTactic> heistTactics = await applicationDbContext.HeistTactics
            .Where(x => x.HeistId == heistId)
            .ToListAsync();

        applicationDbContext.HeistTactics.RemoveRange(heistTactics);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<HeistTactic>> GetHeistTacticsAsync(Guid heistId)
    {
        return await applicationDbContext.HeistTactics
            .Include(x => x.Tactic)
            .Where(x => x.HeistId == heistId)
            .ToListAsync();
    }
}