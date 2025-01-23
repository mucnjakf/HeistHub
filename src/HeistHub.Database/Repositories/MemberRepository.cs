using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace HeistHub.Database.Repositories;

public sealed class MemberRepository(ApplicationDbContext applicationDbContext) : IMemberRepository
{
    public async Task<Guid> CreateAsync(Member member)
    {
        await applicationDbContext.Members.AddAsync(member);
        await applicationDbContext.SaveChangesAsync();

        return member.Id;
    }

    public async Task<bool> ExistsAsync(Guid memberId)
    {
        return await applicationDbContext.Members.AnyAsync(x => x.Id == memberId);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await applicationDbContext.Members.AnyAsync(x => x.Email == email);
    }
}