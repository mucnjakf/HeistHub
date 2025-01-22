using HeistHub.Application.Dtos;

namespace HeistHub.Application.Repositories;

public interface ISkillRepository
{
    Task<IEnumerable<SkillDto>> GetAllByNameAsync(IEnumerable<string> names);

    Task<IEnumerable<SkillDto>> CreateAsync(IEnumerable<MemberSkillDto> skills);

    Task CreateMemberSkillsAsync(Guid memberId, IEnumerable<Guid> skillIds, Guid mainSkillId);
}