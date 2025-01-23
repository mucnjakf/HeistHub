using HeistHub.Application.Dtos;
using HeistHub.Core.Entities;

namespace HeistHub.Application.Repositories;

public interface IHeistRepository
{
    Task<Guid> CreateAsync(Heist heist);

    Task<bool> ExistsAsync(string name);
}