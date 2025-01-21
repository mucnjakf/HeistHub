using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class SkillEntityTypeConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.UseTpcMappingStrategy();

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Level).IsRequired().HasMaxLength(10);
    }
}