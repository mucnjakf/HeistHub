using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class HeistEntityTypeConfiguration : IEntityTypeConfiguration<Heist>
{
    public void Configure(EntityTypeBuilder<Heist> builder)
    {
        builder.ToTable("Heists");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Location).IsRequired();

        builder.Property(x => x.Start).IsRequired();

        builder.Property(x => x.End).IsRequired();

        builder.Property(x => x.Status).IsRequired();

        builder.Property(x => x.IsSuccess).IsRequired();

        builder.HasMany(x => x.HeistTactics).WithOne(x => x.Heist).HasForeignKey(x => x.HeistId);

        builder.HasMany(x => x.Members).WithMany(x => x.Heists);
    }
}