using HeistHub.Application.Commands;
using HeistHub.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeistHub.Api.Endpoints;

public static class HeistEndpoints
{
    public static void MapHeistEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/heists");

        group.MapPost(string.Empty, CreateHeistAsync);
        group.MapPatch("{heistId:guid}/tactics", UpdateHeistTacticsAsync);
    }

    private static async Task<IResult> CreateHeistAsync(HttpContext httpContext, ISender sender, [FromBody] CreateHeistCommand command)
    {
        Guid heistId = await sender.Send(command);

        return Results.Created($"heists/{heistId}", null);
    }

    private static async Task<IResult> UpdateHeistTacticsAsync(
        HttpContext httpContext,
        ISender sender,
        [FromRoute] Guid heistId,
        [FromBody] UpdateHeistTacticsRequest request)
    {
        await sender.Send(new UpdateHeistTacticsCommand(heistId, request.Tactics));

        httpContext.Response.Headers.Location = $"heists/{heistId}/tactics";

        return Results.NoContent();
    }
}