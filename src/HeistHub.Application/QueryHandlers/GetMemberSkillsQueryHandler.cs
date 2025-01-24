using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Queries;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using MediatR;

namespace HeistHub.Application.QueryHandlers;

public sealed class GetMemberSkillsQueryHandler(ISkillRepository skillRepository)
    : IRequestHandler<GetMemberSkillsQuery, MainMemberSkillDto>
{
    public async Task<MainMemberSkillDto> Handle(GetMemberSkillsQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<MemberSkill> memberSkills = (await skillRepository.GetMemberSkillsAsync(query.MemberId)).ToList();

        IEnumerable<MemberSkillDto> skills = memberSkills.Select(x => x.ToMemberSkillDto());
        string mainSkill = memberSkills.FirstOrDefault(x => x.IsMain)?.Skill.Name ?? string.Empty;

        return new MainMemberSkillDto(skills, mainSkill);
    }
}