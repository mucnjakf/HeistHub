namespace HeistHub.Api.Endpoints;

public static class TestEndpoints
{
    public static void MapTestEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("test", GetTest);
    }

    private static IResult GetTest(HttpContext context)
    {
        string test = context.Connection.Id;

        return Results.Ok(new { Message = $"test {test}" });
    }
}