using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeistHub.Database.Context;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Heist> Heists { get; set; }

    public DbSet<HeistSkill> HeistSkills { get; set; }

    public DbSet<Member> Members { get; set; }

    public DbSet<MemberSkill> MemberSkills { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}