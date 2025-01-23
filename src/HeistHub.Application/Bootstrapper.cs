using System.Reflection;
using FluentValidation;
using HeistHub.Application.Services;
using HeistHub.Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace HeistHub.Application;

public static class Bootstrapper
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        Assembly assembly = typeof(Bootstrapper).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ITacticService, TacticService>();

        return services;
    }
}