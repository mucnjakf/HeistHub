using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class MemberSkillEntityTypeConfiguration : IEntityTypeConfiguration<MemberSkill>
{
    public void Configure(EntityTypeBuilder<MemberSkill> builder)
    {
        builder.ToTable("MemberSkills");

        builder.HasKey(x => new { x.MemberId, x.SkillId });

        builder.Property(x => x.IsMain).IsRequired();
    }
}