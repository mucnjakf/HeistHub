using HeistHub.Application.Dtos;

namespace HeistHub.Application.Services;

public interface ITacticService
{
    Task<List<TacticDto>> CreateTacticsAsync(List<HeistTacticDto> tactics);
}