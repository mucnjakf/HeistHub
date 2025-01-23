using HeistHub.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeistHub.Api.Endpoints;

public static class HeistEndpoints
{
    public static void MapHeistEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/heists");

        group.MapPost(string.Empty, CreateHeistAsync);
    }

    private static async Task<IResult> CreateHeistAsync(HttpContext context, ISender sender, [FromBody] CreateHeistCommand command)
    {
        Guid heistId = await sender.Send(command);

        return Results.Created($"heists/{heistId}", null);
    }
}