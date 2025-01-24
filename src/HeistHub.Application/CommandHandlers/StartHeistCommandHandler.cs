using HeistHub.Application.Commands;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using HeistHub.Core.Enums;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class StartHeistCommandHandler(IHeistRepository heistRepository) : IRequestHandler<StartHeistCommand>
{
    public async Task Handle(StartHeistCommand command, CancellationToken cancellationToken)
    {
        Heist heist = await heistRepository.GetAsync(command.HeistId);

        if (heist.Status is not HeistStatus.Ready)
        {
            throw new HeistNotReadyException("Cannot start a heist that is not ready.");
        }

        await heistRepository.UpdateStatusAsync(heist, HeistStatus.InProgress);
    }
}