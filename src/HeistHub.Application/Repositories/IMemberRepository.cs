using HeistHub.Core.Entities;

namespace HeistHub.Application.Repositories;

public interface IMemberRepository
{
    Task<Guid> CreateAsync(Member member);

    Task<bool> EmailExistsAsync(string email);
}