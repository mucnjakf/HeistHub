﻿using System.Text.Json.Serialization;
using HeistHub.Application;
using HeistHub.Database;

namespace HeistHub.Api;

public static class Bootstrapper
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddOpenApi();

        services
            .AddApplication()
            .AddDatabase(configuration);
        
        return services;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "HeistHub API"));
        }

        app.UseHttpsRedirection();

        return app;
    }
}