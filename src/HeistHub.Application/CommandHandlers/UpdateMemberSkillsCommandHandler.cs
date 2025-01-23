using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class UpdateMemberSkillsCommandHandler(IMemberRepository memberRepository, ISkillRepository skillRepository)
    : IRequestHandler<UpdateMemberSkillsCommand>
{
    public async Task Handle(UpdateMemberSkillsCommand command, CancellationToken cancellationToken)
    {
        bool memberExists = await memberRepository.ExistsAsync(command.MemberId);

        if (!memberExists)
        {
            throw new MemberNotFoundException($"Member with ID {command.MemberId} not found.");
        }

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
        List<SkillDto> newSkills = await CreateSkillsAsync(command.Skills.ToList());
        Guid mainSkillId = Guid.Empty;

        if (command.MainSkill is not null)
        {
            mainSkillId = newSkills.FirstOrDefault(x => x.Name == command.MainSkill)!.Id;
        }

        await skillRepository.CreateMemberSkillsAsync(command.MemberId, newSkills.Select(x => x.Id), mainSkillId);
    }

    // TODO: move to service
    private async Task<List<SkillDto>> CreateSkillsAsync(List<MemberSkillDto> skills)
    {
        IEnumerable<SkillDto> existingSkills = await skillRepository.GetAllByNameAndLevelAsync(skills);

        IEnumerable<MemberSkillDto> nonExistingSkills = skills
            .Where(x => !existingSkills.Any(y => y.Name == x.Name && y.Level == x.Level));

        IEnumerable<SkillDto> newSkills = await skillRepository.CreateAsync(nonExistingSkills);

        return newSkills.Concat(existingSkills).ToList();
    }
}