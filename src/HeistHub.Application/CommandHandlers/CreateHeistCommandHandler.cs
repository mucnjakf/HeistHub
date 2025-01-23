using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Repositories;
using HeistHub.Application.Services;
using HeistHub.Core.Entities;
using MediatR;

namespace HeistHub.Application.CommandHandlers;

public sealed class CreateHeistCommandHandler(IHeistRepository heistRepository, ITacticRepository tacticRepository, ITacticService tacticService)
    : IRequestHandler<CreateHeistCommand, Guid>
{
    public async Task<Guid> Handle(CreateHeistCommand command, CancellationToken cancellationToken)
    {
        Heist heist = Heist.Create(command.Name, command.Location, command.Start, command.End);
        Guid heistId = await heistRepository.CreateAsync(heist);

        List<TacticDto> newTactics = await tacticService.CreateTacticsAsync(command.Tactics.ToList());

        await tacticRepository.CreateHeistTacticsAsync(heistId, newTactics.Select(x => x.Id));

        return heistId;
    }
}