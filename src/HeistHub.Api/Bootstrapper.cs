using System.Text.Json.Serialization;
using HeistHub.Api.Endpoints;
using HeistHub.Api.Handlers;
using HeistHub.Application;
using HeistHub.Database;

namespace HeistHub.Api;

public static class Bootstrapper
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddOpenApi();

        services.AddHttpExceptionHandler();

        services
            .AddApplication()
            .AddDatabase(configuration);

        return services;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.MapEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "HeistHub API"));
        }

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        return app;
    }

    private static IServiceCollection AddHttpExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<HttpExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    private static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapMemberEndpoints();
        app.MapHeistEndpoints();

        return app;
    }
}