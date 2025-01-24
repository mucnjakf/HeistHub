using HeistHub.Core.Entities;
using HeistHub.Core.Enums;

namespace HeistHub.Application.Repositories;

public interface IHeistRepository
{
    Task<Heist> GetAsync(Guid heistId);

    Task<Guid> CreateAsync(Heist heist);

    Task<bool> ExistsAsync(string name);

    Task<bool> ExistsAsync(Guid heistId);

    Task<bool> DidHeistStartAsync(Guid heistId);

    Task UpdateStatusAsync(Heist heist, HeistStatus status);
}