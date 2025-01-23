using HeistHub.Application.Dtos;

namespace HeistHub.Application.Services;

public interface ISkillService
{
    Task<List<SkillDto>> CreateSkillsAsync(List<MemberSkillDto> skills);
}