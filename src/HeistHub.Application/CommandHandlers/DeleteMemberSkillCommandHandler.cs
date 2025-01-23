using HeistHub.Application.Commands;
using HeistHub.Application.Repositories;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class DeleteMemberSkillCommandHandler(IMemberRepository memberRepository, ISkillRepository skillRepository)
    : IRequestHandler<DeleteMemberSkillCommand>
{
    public async Task Handle(DeleteMemberSkillCommand command, CancellationToken cancellationToken)
    {
        bool memberExists = await memberRepository.ExistsAsync(command.MemberId);

        if (!memberExists)
        {
            throw new MemberNotFoundException($"Member with ID {command.MemberId} not found.");
        }

        await skillRepository.DeleteMemberSkillAsync(command.MemberId, command.SkillName);
    }
}