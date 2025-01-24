using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Repositories;

public interface ISkillRepository
{
    Task<IEnumerable<SkillDto>> GetAllByNameAndLevelAsync(List<MemberSkillDto> skills);

    Task<IEnumerable<SkillDto>> CreateAsync(IEnumerable<MemberSkillDto> skills);

    Task CreateMemberSkillsAsync(Guid memberId, IEnumerable<Guid> skillIds, Guid mainSkillId);

    Task RemoveMemberSkillsAsync(Guid memberId);

    Task<Guid> GetIdByNameAsync(string skillName);

    Task UpdateMemberMainSkillAsync(Guid memberId, Guid mainSkillId);

    Task DeleteMemberSkillAsync(Guid memberId, string skillName);

    Task<IEnumerable<MemberSkill>> GetMemberSkillsAsync(Guid memberId);
}