using HeistHub.Core.Enums;

namespace HeistHub.Core.Entities;

public sealed class Member
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public Gender Gender { get; private set; }

    public string Email { get; private set; } = null!;

    public MemberStatus Status { get; private set; }

    public IEnumerable<MemberSkill>? MemberSkills { get; private set; }
    
    public IEnumerable<Heist>? Heists { get; private set; }

    private Member()
    {
    }

    private Member(
        Guid id,
        string name,
        Gender gender,
        string email,
        MemberStatus status)
    {
        Id = id;
        Name = name;
        Gender = gender;
        Email = email;
        Status = status;
    }

    public static Member Create(
        string name,
        Gender gender,
        string email,
        MemberStatus status)
    {
        Guid id = Guid.NewGuid();

        return new Member(id, name, gender, email, status);
    }
}