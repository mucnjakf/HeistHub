using HeistHub.Application.Dtos;

namespace HeistHub.Application.Repositories;

public interface ITacticRepository
{
    Task<IEnumerable<TacticDto>> GetAllByNameLevelAndMembersRequiredAsync(List<HeistTacticDto> tactics);

    Task<IEnumerable<TacticDto>> CreateAsync(IEnumerable<HeistTacticDto> tactics);

    Task CreateHeistTacticsAsync(Guid heistId, IEnumerable<Guid> tacticIds);
}