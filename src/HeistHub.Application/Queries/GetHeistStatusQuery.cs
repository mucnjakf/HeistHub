using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Queries;

public sealed record GetHeistStatusQuery(Guid HeistId) : IRequest<HeistStatusDto>;