﻿using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;
using HeistHub.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace HeistHub.Api.Handlers;

public sealed class HttpExceptionHandler(ILogger<HttpExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ErrorResponseDto errorResponse = GetErrorResponse(exception);

        logger.LogError("Http Status Code {httpStatusCode} - Error message: {Message}",
            errorResponse.HttpStatusCode, errorResponse.Message);

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = (int)errorResponse.HttpStatusCode;

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }

    private static ErrorResponseDto GetErrorResponse(Exception exception) => exception switch
    {
        ValidationException vex => new ErrorResponseDto(HttpStatusCode.BadRequest, vex.Message, vex.Errors),
        
        _ => new ErrorResponseDto(HttpStatusCode.InternalServerError, "Unhandled exception occured.")
    };

    private sealed record ErrorResponseDto(
        [property: JsonIgnore] HttpStatusCode HttpStatusCode,
        string Message,
        IEnumerable<string>? ValidationErrors = null);
}