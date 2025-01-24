using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;

namespace HeistHub.Application.Services;

public sealed class TacticService(ITacticRepository tacticRepository) : ITacticService
{
    public async Task<List<TacticDto>> CreateTacticsAsync(List<HeistTacticDto> tactics)
    {
        List<TacticDto> existingTactics = (await tacticRepository.GetAllByNameLevelAndMembersRequiredAsync(tactics)).ToList();
        
        IEnumerable<HeistTacticDto> nonExistingTactics = tactics
            .Where(x => !existingTactics.AsEnumerable().Any(y => y.Name == x.Name && y.Level == x.Level));

        IEnumerable<TacticDto> newTactics = await tacticRepository.CreateAsync(nonExistingTactics);

        return newTactics.Concat(existingTactics).ToList();
    }
}