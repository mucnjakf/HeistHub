using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace HeistHub.Database.Repositories;

public sealed class SkillRepository(ApplicationDbContext applicationDbContext) : ISkillRepository
{
    public async Task<IEnumerable<SkillDto>> GetAllByNameAsync(IEnumerable<string> names)
    {
        return await applicationDbContext.Skills
            .Where(x => names.Contains(x.Name))
            .Select(x => x.ToSkillDto())
            .ToListAsync();
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
}