using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Queries;
using HeistHub.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HeistHub.Api.Endpoints;

public static class HeistEndpoints
{
    public static void MapHeistEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/heists");

        group.MapGet("{heistId:guid}", GetHeistAsync);
        group.MapGet("{heistId:guid}/tactics", GetHeistTacticsAsync);
        group.MapGet("{heistId:guid}/members", GetHeistMembersAsync);
        group.MapGet("{heistId:guid}/status", GetHeistStatusAsync);
        group.MapPost(string.Empty, CreateHeistAsync);
        group.MapPatch("{heistId:guid}/tactics", UpdateHeistTacticsAsync);
        group.MapPut("{heistId:guid}/start", StartHeistAsync);
    }

    private static async Task<IResult> GetHeistAsync(HttpContext httpContext, ISender sender, [FromRoute] Guid heistId)
    {
        HeistDto heist = await sender.Send(new GetHeistQuery(heistId));

        return Results.Ok(heist);
    }

    private static async Task<IResult> GetHeistTacticsAsync(HttpContext httpContext, ISender sender, [FromRoute] Guid heistId)
    {
        IEnumerable<HeistTacticDto> heistTactics = await sender.Send(new GetHeistTacticsQuery(heistId));

        return Results.Ok(heistTactics);
    }

    private static async Task<IResult> GetHeistMembersAsync(HttpContext httpContext, ISender sender, [FromRoute] Guid heistId)
    {
        IEnumerable<HeistMemberDto> heistMembers = await sender.Send(new GetHeistMembersQuery(heistId));

        return Results.Ok(heistMembers);
    }

    private static async Task<IResult> GetHeistStatusAsync(HttpContext httpContext, ISender sender, [FromRoute] Guid heistId)
    {
        HeistStatusDto heistStatus = await sender.Send(new GetHeistStatusQuery(heistId));

        return Results.Ok(heistStatus);
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

    private static async Task<IResult> StartHeistAsync(HttpContext httpContext, ISender sender, [FromRoute] Guid heistId)
    {
        await sender.Send(new StartHeistCommand(heistId));

        httpContext.Response.Headers.Location = $"heists/{heistId}/status";

        return Results.Ok();
    }
}