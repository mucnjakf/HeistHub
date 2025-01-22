using FluentValidation;
using FluentValidation.Results;
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

        IEnumerable<Task<ValidationResult>> validationTasks = validators.Select(validator => validator.ValidateAsync(request, cancellationToken));
        ValidationResult[] validationResults = await Task.WhenAll(validationTasks);

        List<string> errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .Select(error => error.ErrorMessage)
            .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException($"{typeof(TRequest).Name} is invalid", errors);
        }

        return await next();
    }
}