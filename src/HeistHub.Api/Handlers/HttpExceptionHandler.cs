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

        logger.LogError("Http status code: {httpStatusCode} - Error message: {Message}",
            errorResponse.HttpStatusCode, errorResponse.Message);

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = (int)errorResponse.HttpStatusCode;

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }

    private static ErrorResponseDto GetErrorResponse(Exception exception) => exception switch
    {
        DuplicateSkillException dse => new ErrorResponseDto(HttpStatusCode.BadRequest, dse.Message),
        DuplicateTacticException dte => new ErrorResponseDto(HttpStatusCode.BadRequest, dte.Message),
        ValidationException ve => new ErrorResponseDto(HttpStatusCode.BadRequest, ve.Message, ve.Errors),

        MemberNotFoundException mnfe => new ErrorResponseDto(HttpStatusCode.NotFound, mnfe.Message),
        MemberSkillNotFoundException snfe => new ErrorResponseDto(HttpStatusCode.NotFound, snfe.Message),
        HeistNotFoundException hnfe => new ErrorResponseDto(HttpStatusCode.NotFound, hnfe.Message),

        HeistStartedException hse => new ErrorResponseDto(HttpStatusCode.MethodNotAllowed, hse.Message),
        HeistNotReadyException hnre => new ErrorResponseDto(HttpStatusCode.MethodNotAllowed, hnre.Message),
        HeistStatusException hse => new ErrorResponseDto(HttpStatusCode.MethodNotAllowed, hse.Message),

        _ => new ErrorResponseDto(HttpStatusCode.InternalServerError, "Unhandled exception occured.")
    };

    private sealed record ErrorResponseDto(
        [property: JsonIgnore] HttpStatusCode HttpStatusCode,
        string Message,
        IEnumerable<string>? ValidationErrors = null);
}