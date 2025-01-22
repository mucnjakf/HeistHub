using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class SkillEntityTypeConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();
        
        builder.Property(x => x.Level).IsRequired().HasMaxLength(10);

        builder.HasMany(x => x.MemberSkills).WithOne(x => x.Skill).HasForeignKey(x => x.SkillId);
    }
}