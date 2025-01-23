using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
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
}