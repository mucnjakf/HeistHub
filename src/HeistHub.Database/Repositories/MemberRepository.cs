using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Core.Exceptions;
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

    public async Task<Member> GetAsync(Guid memberId)
    {
        Member? member = await applicationDbContext.Members
            .Include(x => x.MemberSkills)!
            .ThenInclude(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == memberId);

        if (member is null)
        {
            throw new MemberNotFoundException($"Member with ID {memberId} not found.");
        }

        return member;
    }
}