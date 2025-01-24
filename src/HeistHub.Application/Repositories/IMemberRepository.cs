using HeistHub.Core.Entities;

namespace HeistHub.Application.Repositories;

public interface IMemberRepository
{
    Task<Guid> CreateAsync(Member member);

    Task<bool> ExistsAsync(Guid memberId);

    Task<bool> EmailExistsAsync(string email);

    Task<Member> GetAsync(Guid memberId);
}