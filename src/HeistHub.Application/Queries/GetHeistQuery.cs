using HeistHub.Application.Dtos;
using MediatR;

namespace HeistHub.Application.Queries;

public sealed record GetHeistQuery(Guid HeistId) : IRequest<HeistDto>;