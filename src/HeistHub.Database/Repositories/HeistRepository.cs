using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Core.Enums;
using HeistHub.Core.Exceptions;
using HeistHub.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace HeistHub.Database.Repositories;

public class HeistRepository(ApplicationDbContext applicationDbContext) : IHeistRepository
{
    public async Task<Guid> CreateAsync(Heist heist)
    {
        await applicationDbContext.Heists.AddAsync(heist);
        await applicationDbContext.SaveChangesAsync();

        return heist.Id;
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await applicationDbContext.Heists.AnyAsync(x => x.Name == name);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await applicationDbContext.Heists.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> DidHeistStartAsync(Guid heistId)
    {
        Heist? heist = await applicationDbContext.Heists.FirstOrDefaultAsync(x => x.Id == heistId);

        if (heist is null)
        {
            throw new HeistNotFoundException($"Heist with ID {heistId} not found.");
        }

        return heist.Status is HeistStatus.InProgress;
    }
}