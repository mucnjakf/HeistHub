using HeistHub.Application.Commands;
using HeistHub.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HeistHub.Api.Endpoints;

public static class MemberEndpoints
{
    public static void MapMemberEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/members");

        group.MapPost(string.Empty, CreateMemberAsync);
        group.MapPut("{memberId:guid}/skills", UpdateMemberSkillsAsync);
        group.MapDelete("{memberId:guid}/skills/{skillName}", DeleteMemberSkillAsync);
    }

    private static async Task<IResult> CreateMemberAsync(HttpContext httpContext, ISender sender, [FromBody] CreateMemberCommand command)
    {
        Guid memberId = await sender.Send(command);

        return Results.Created($"members/{memberId}", null);
    }

    private static async Task<IResult> UpdateMemberSkillsAsync(
        HttpContext httpContext,
        ISender sender,
        [FromRoute] Guid memberId,
        [FromBody] UpdateMemberSkillsRequest request)
    {
        await sender.Send(new UpdateMemberSkillsCommand(memberId, request.Skills, request.MainSkill));

        httpContext.Response.Headers.Location = $"members/{memberId}";

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteMemberSkillAsync(
        HttpContext httpContext,
        ISender sender,
        [FromRoute] Guid memberId,
        [FromRoute] string skillName)
    {
        await sender.Send(new DeleteMemberSkillCommand(memberId, skillName));

        return Results.NoContent();
    }
}