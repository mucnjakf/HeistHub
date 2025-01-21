using HeistHub.Core.Enums;

namespace HeistHub.Core.Entities;

public sealed class Member
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public Gender Gender { get; private set; }

    public string Email { get; private set; } = null!; // TODO : unique

    public MemberStatus Status { get; private set; }

    public IEnumerable<MemberSkill> Skills { get; private set; } = null!;

    public IEnumerable<Heist>? Heists { get; private set; }

    private Member()
    {
    }

    private Member(
        Guid id,
        string name,
        Gender gender,
        string email,
        MemberStatus status,
        IEnumerable<MemberSkill> skills)
    {
        Id = id;
        Name = name;
        Gender = gender;
        Email = email;
        Status = status;
        Skills = skills;
    }

    public static Member Create(
        string name,
        Gender gender,
        string email,
        MemberStatus status,
        IEnumerable<MemberSkill> skills)
    {
        Guid id = Guid.NewGuid();

        return new Member(id, name, gender, email, status, skills);
    }
}