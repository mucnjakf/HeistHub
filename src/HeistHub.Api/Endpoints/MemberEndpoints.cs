using HeistHub.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeistHub.Api.Endpoints;

public static class MemberEndpoints
{
    public static void MapMemberEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/members");

        group.MapPost(string.Empty, CreateMemberAsync);
    }

    private static async Task<IResult> CreateMemberAsync(HttpContext httpContext, ISender sender, [FromBody] CreateMemberCommand command)
    {
        Guid memberId = await sender.Send(command);

        return Results.Created($"members/{memberId}", null);
    }
}