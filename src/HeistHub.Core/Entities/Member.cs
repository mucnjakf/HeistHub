using HeistHub.Core.Enums;

namespace HeistHub.Core.Entities;

public sealed class Member : Entity
{
    public string Name { get; private set; }

    public Gender Gender { get; private set; }

    public string Email { get; private set; } // TODO : unique

    public MemberStatus Status { get; set; }

    public IEnumerable<MemberSkill> Skills { get; private set; }

    public IEnumerable<Heist>? Heists { get; private set; }

    private Member(
        Guid id,
        string name,
        Gender gender,
        string email,
        MemberStatus status,
        IEnumerable<MemberSkill> skills) : base(id)
    {
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