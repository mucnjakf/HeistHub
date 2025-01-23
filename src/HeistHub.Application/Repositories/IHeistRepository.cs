using HeistHub.Core.Entities;

namespace HeistHub.Application.Repositories;

public interface IHeistRepository
{
    Task<Guid> CreateAsync(Heist heist);

    Task<bool> ExistsAsync(string name);

    Task<bool> ExistsAsync(Guid id);

    Task<bool> DidHeistStartAsync(Guid heistId);
}