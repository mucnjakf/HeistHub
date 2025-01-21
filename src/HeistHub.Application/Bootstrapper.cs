using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HeistHub.Application;

public static class Bootstrapper
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(Bootstrapper).Assembly;

        services.AddMediatR(configuration => { configuration.RegisterServicesFromAssembly(assembly); });

        return services;
    }
}