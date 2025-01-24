using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Core.Exceptions;
using HeistHub.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace HeistHub.Database.Repositories;

public sealed class SkillRepository(ApplicationDbContext applicationDbContext) : ISkillRepository
{
    public async Task<IEnumerable<SkillDto>> GetAllByNameAndLevelAsync(List<MemberSkillDto> skills)
    {
        List<Skill> dbSkills = await applicationDbContext.Skills.ToListAsync();

        return dbSkills
            .Where(x => skills.Any(y => y.Name == x.Name && y.Level == x.Level))
            .Select(x => x.ToSkillDto());
    }

    public async Task<IEnumerable<SkillDto>> CreateAsync(IEnumerable<MemberSkillDto> skills)
    {
        List<Skill> newSkills = [];

        newSkills.AddRange(skills.Select(skill => Skill.Create(skill.Name, skill.Level)));

        await applicationDbContext.Skills.AddRangeAsync(newSkills);
        await applicationDbContext.SaveChangesAsync();

        return newSkills.Select(x => x.ToSkillDto());
    }

    public async Task CreateMemberSkillsAsync(Guid memberId, IEnumerable<Guid> skillIds, Guid mainSkillId)
    {
        IEnumerable<MemberSkill> memberSkills = skillIds
            .Select(skillId => new MemberSkill { MemberId = memberId, SkillId = skillId, IsMain = skillId == mainSkillId });

        await applicationDbContext.MemberSkills
            .AddRangeAsync(memberSkills);

        await applicationDbContext.SaveChangesAsync();
    }

    public async Task RemoveMemberSkillsAsync(Guid memberId)
    {
        List<MemberSkill> memberSkills = await applicationDbContext.MemberSkills
            .Where(x => x.MemberId == memberId)
            .ToListAsync();

        applicationDbContext.MemberSkills.RemoveRange(memberSkills);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<Guid> GetIdByNameAsync(string skillName)
    {
        MemberSkill? memberSkill = await applicationDbContext.MemberSkills
            .Include(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Skill.Name == skillName);

        if (memberSkill is null)
        {
            throw new MemberSkillNotFoundException($"Member does not have a skill with name {skillName}.");
        }

        return memberSkill.SkillId;
    }

    public async Task UpdateMemberMainSkillAsync(Guid memberId, Guid mainSkillId)
    {
        List<MemberSkill> memberSkills = await applicationDbContext.MemberSkills
            .Where(x => x.MemberId == memberId)
            .ToListAsync();

        memberSkills.First(x => x.IsMain).IsMain = false;
        memberSkills.First(x => x.SkillId == mainSkillId).IsMain = true;

        await applicationDbContext.SaveChangesAsync();
    }

    public async Task DeleteMemberSkillAsync(Guid memberId, string skillName)
    {
        MemberSkill? memberSkill = await applicationDbContext.MemberSkills
            .Include(x => x.Skill)
            .FirstOrDefaultAsync(x => x.MemberId == memberId && x.Skill.Name == skillName);

        if (memberSkill is null)
        {
            throw new MemberSkillNotFoundException($"Member does not have a skill with name {skillName}.");
        }

        applicationDbContext.MemberSkills.Remove(memberSkill);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<MemberSkill>> GetMemberSkillsAsync(Guid memberId)
    {
        return await applicationDbContext.MemberSkills
            .Include(x => x.Skill)
            .Where(x => x.MemberId == memberId)
            .ToListAsync();
    }
}