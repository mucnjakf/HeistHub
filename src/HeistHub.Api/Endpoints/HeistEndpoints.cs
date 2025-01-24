﻿using HeistHub.Application.Commands;
using HeistHub.Application.Dtos;
using HeistHub.Application.Queries;
using HeistHub.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeistHub.Api.Endpoints;

public static class HeistEndpoints
{
    public static void MapHeistEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/heists");

        group.MapGet("{heistId:guid}", GetHeistAsync);
        group.MapPost(string.Empty, CreateHeistAsync);
        group.MapPatch("{heistId:guid}/tactics", UpdateHeistTacticsAsync);
        group.MapPut("{heistId:guid}/start", StartHeistAsync);
    }

    private static async Task<IResult> GetHeistAsync(HttpContext httpContext, ISender sender, [FromRoute] Guid heistId)
    {
        HeistDto heist = await sender.Send(new GetHeistQuery(heistId));

        return Results.Ok(heist);
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