using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Repositories;

public interface ITacticRepository
{
    Task<IEnumerable<TacticDto>> GetAllByNameLevelAndMembersRequiredAsync(List<HeistTacticDto> tactics);

    Task<IEnumerable<TacticDto>> CreateAsync(IEnumerable<HeistTacticDto> tactics);

    Task CreateHeistTacticsAsync(Guid heistId, IEnumerable<Guid> tacticIds);

    Task RemoveHeistTacticsAsync(Guid heistId);

    Task<IEnumerable<HeistTactic>> GetHeistTacticsAsync(Guid heistId);
}