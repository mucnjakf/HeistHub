using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Mappers;

public static class SkillMapper
{
    public static SkillDto ToSkillDto(this Skill skill)
    {
        return new SkillDto(skill.Id, skill.Name, skill.Level);
    }   
}