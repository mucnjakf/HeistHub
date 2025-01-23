using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;
using HeistHub.Application.Services;
using HeistHub.Core.Entities;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class CreateMemberCommandHandler(
    IMemberRepository memberRepository,
    ISkillRepository skillRepository,
    ISkillService skillService)
    : IRequestHandler<CreateMemberCommand, Guid>
{
    public async Task<Guid> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        Member member = Member.Create(command.Name, command.Gender, command.Email, command.Status);
        Guid memberId = await memberRepository.CreateAsync(member);

        List<SkillDto> newSkills = await skillService.CreateSkillsAsync(command.Skills.ToList());

        SkillDto? mainSkill = newSkills.FirstOrDefault(x => x.Name == command.MainSkill);

        if (mainSkill is null)
        {
            throw new MemberSkillNotFoundException("Main skill not found.");
        }

        await skillRepository.CreateMemberSkillsAsync(memberId, newSkills.Select(x => x.Id), mainSkill.Id);

        return memberId;
    }
}