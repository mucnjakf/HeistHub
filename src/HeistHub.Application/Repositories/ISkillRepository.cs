using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;

namespace HeistHub.Application.Repositories;

public interface ISkillRepository
{
    Task<IEnumerable<SkillDto>> GetAllByNameAsync(IEnumerable<string> names);

    Task<IEnumerable<SkillDto>> CreateAsync(IEnumerable<CreateMemberSkillCommand> skills);

    Task CreateMemberSkillsAsync(Guid memberId, IEnumerable<Guid> skillIds, Guid mainSkillId);
}