using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;
using HeistHub.Application.Services;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class UpdateMemberSkillsCommandHandler(
    ISkillRepository skillRepository,
    ISkillService skillService)
    : IRequestHandler<UpdateMemberSkillsCommand>
{
    public async Task Handle(UpdateMemberSkillsCommand command, CancellationToken cancellationToken)
    {
        // TODO 404 if not found
        
        if (command.Skills is null)
        {
            Guid skillId = await skillRepository.GetIdByNameAsync(command.MainSkill!);
            await skillRepository.UpdateMemberMainSkillAsync(command.MemberId, skillId);
            return;
        }

        bool duplicateSkills = command.Skills
            .GroupBy(x => x.Name)
            .Any(x => x.Count() > 1);

        if (duplicateSkills)
        {
            throw new DuplicateSkillException("Duplicate skill names are not allowed.");
        }

        await skillRepository.RemoveMemberSkillsAsync(command.MemberId);
        List<SkillDto> newSkills = await skillService.CreateSkillsAsync(command.Skills.ToList());
        Guid mainSkillId = Guid.Empty;

        if (command.MainSkill is not null)
        {
            mainSkillId = newSkills.FirstOrDefault(x => x.Name == command.MainSkill)!.Id;
        }

        await skillRepository.CreateMemberSkillsAsync(command.MemberId, newSkills.Select(x => x.Id), mainSkillId);
    }
}