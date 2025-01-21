using FluentValidation;
using MediatR;
using ValidationException = HeistHub.Core.Exceptions.ValidationException;

namespace HeistHub.Application.Validation;

public sealed class ValidationPipelineBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        List<string> errors = validators.Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException($"{typeof(TRequest).Name} is invalid", errors);
        }

        return await next();
    }
}