using HeistHub.Application.Repositories;
using HeistHub.Database.Context;
using HeistHub.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeistHub.Database;

public static class Bootstrapper
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        string connectionString = configuration.GetConnectionString("Default")!;

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IHeistRepository, HeistRepository>();
        services.AddScoped<ITacticRepository, TacticRepository>();

        return services;
    }
}