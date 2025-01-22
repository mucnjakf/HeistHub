using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class CreateMemberCommandHandler(IMemberRepository memberRepository, ISkillRepository skillRepository)
    : IRequestHandler<CreateMemberCommand, Guid>
{
    public async Task<Guid> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        Member member = Member.Create(command.Name, command.Gender, command.Email, command.Status);
        Guid memberId = await memberRepository.CreateAsync(member);

        List<SkillDto> newSkills = await CreateSkillsAsync(command.Skills.ToList());
        Guid mainSkillId = newSkills.FirstOrDefault(x => x.Name == command.MainSkill)!.Id;

        await skillRepository.CreateMemberSkillsAsync(memberId, newSkills.Select(x => x.Id), mainSkillId);

        return memberId;
    }

    private async Task<List<SkillDto>> CreateSkillsAsync(List<CreateMemberSkillCommand> skills)
    {
        IEnumerable<string> skillNames = skills.Select(x => x.Name);
        IEnumerable<SkillDto> existingSkills = await skillRepository.GetAllByNameAsync(skillNames);

        IEnumerable<CreateMemberSkillCommand> nonExistingSkills = skills
            .Where(x => !existingSkills
                .Select(y => y.Name)
                .Contains(x.Name));

        IEnumerable<SkillDto> newSkills = await skillRepository.CreateAsync(nonExistingSkills);

        return newSkills.Concat(existingSkills).ToList();
    }
}