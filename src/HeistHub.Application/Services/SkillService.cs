using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;

namespace HeistHub.Application.Services;

public sealed class SkillService(ISkillRepository skillRepository) : ISkillService
{
    // TODO: maybe mobe to repo
    public async Task<List<SkillDto>> CreateSkillsAsync(List<MemberSkillDto> skills)
    {
        List<SkillDto> existingSkills = (await skillRepository.GetAllByNameAndLevelAsync(skills)).ToList();

        IEnumerable<MemberSkillDto> nonExistingSkills = skills
            .Where(x => !existingSkills.AsEnumerable().Any(y => y.Name == x.Name && y.Level == x.Level));

        IEnumerable<SkillDto> newSkills = await skillRepository.CreateAsync(nonExistingSkills);

        return newSkills.Concat(existingSkills).ToList();
    }
}