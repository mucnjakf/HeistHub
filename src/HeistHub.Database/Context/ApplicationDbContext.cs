using HeistHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeistHub.Database.Context;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Heist> Heists { get; set; }

    public DbSet<Tactic> Tactics { get; set; }

    public DbSet<HeistTactic> HeistTactics { get; set; }

    public DbSet<Member> Members { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<MemberSkill> MemberSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}