using MediatR;

namespace HeistHub.Application.Commands;

public sealed record StartHeistCommand(Guid HeistId) : IRequest;