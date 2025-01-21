using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeistHub.Database.Configurations;

public sealed class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired();
        
        builder.Property(x => x.Gender).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).IsRequired();
        
        builder.Property(x => x.Status).IsRequired();

        builder.HasMany(x => x.Skills).WithMany();
    }
}