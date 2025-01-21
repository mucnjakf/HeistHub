using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class HeistSkillEntityTypeConfiguration : IEntityTypeConfiguration<HeistSkill>
{
    public void Configure(EntityTypeBuilder<HeistSkill> builder)
    {
        builder.ToTable("HeistSkills");

        builder.Property(x => x.MembersRequired).IsRequired();
    }
}