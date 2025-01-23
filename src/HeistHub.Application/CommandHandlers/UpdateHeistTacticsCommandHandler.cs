using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;
using HeistHub.Application.Services;
using HeistHub.Core.Exceptions;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class UpdateHeistTacticsCommandHandler(
    IHeistRepository heistRepository,
    ITacticRepository tacticRepository,
    ITacticService tacticService)
    : IRequestHandler<UpdateHeistTacticsCommand>
{
    // TODO: validation
    public async Task Handle(UpdateHeistTacticsCommand command, CancellationToken cancellationToken)
    {
        bool heistStarted = await heistRepository.DidHeistStartAsync(command.HeistId);

        if (heistStarted)
        {
            throw new HeistStartedException("Cannot update ongoing heist tactics.");
        }

        bool duplicateTactics = command.Tactics
            .GroupBy(x => new { x.Name, x.Level })
            .Any(x => x.Count() > 1);

        if (duplicateTactics)
        {
            throw new DuplicateTacticException("Duplicate tactics are not allowed.");
        }

        await tacticRepository.RemoveHeistTacticsAsync(command.HeistId);
        List<TacticDto> newTactics = await tacticService.CreateTacticsAsync(command.Tactics.ToList());

        await tacticRepository.CreateHeistTacticsAsync(command.HeistId, newTactics.Select(x => x.Id));
    }
}