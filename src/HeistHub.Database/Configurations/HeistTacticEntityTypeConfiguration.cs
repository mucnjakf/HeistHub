using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class HeistTacticEntityTypeConfiguration : IEntityTypeConfiguration<HeistTactic>
{
    public void Configure(EntityTypeBuilder<HeistTactic> builder)
    {
        builder.ToTable("HeistTactics");

        builder.HasKey(x => new { x.HeistId, x.TacticId });
    }
}