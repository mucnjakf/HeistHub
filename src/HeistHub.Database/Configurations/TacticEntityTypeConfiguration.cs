using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class TacticEntityTypeConfiguration : IEntityTypeConfiguration<Tactic>
{
    public void Configure(EntityTypeBuilder<Tactic> builder)
    {
        builder.ToTable("Tactics");

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Level).IsRequired().HasMaxLength(10);

        builder.HasMany(x => x.HeistTactics).WithOne(x => x.Tactic).HasForeignKey(x => x.TacticId);
    }
}