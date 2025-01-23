using FluentValidation;
using HeistHub.Application.Commands;
using HeistHub.Application.Repositories;

namespace HeistHub.Application.Validation.Validators;

public sealed class CreateHeistCommandValidator : AbstractValidator<CreateHeistCommand>
{
    public CreateHeistCommandValidator(IHeistRepository heistRepository, TimeProvider timeProvider)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(async (name, _) => !await heistRepository.ExistsAsync(name))
            .WithMessage("Heist with this name already exists.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.");

        RuleFor(x => x.Start)
            .NotEmpty().WithMessage("Start is required.");

        RuleFor(x => x.End)
            .NotEmpty().WithMessage("End is required.")
            .Must((command, end) => end > command.Start)
            .WithMessage("The end time must be after the start time.")
            .Must(end => end > timeProvider.GetUtcNow())
            .WithMessage("The end time must not be in the past.");

        RuleFor(x => x.Tactics)
            .NotEmpty().WithMessage("At least one tactic is required.")
            .ForEach(x =>
            {
                x.ChildRules(tacticValidator =>
                {
                    tacticValidator.RuleFor(y => y.Name)
                        .NotEmpty()
                        .WithMessage("Tactic name is required.");

                    tacticValidator.RuleFor(y => y.Level)
                        .NotEmpty()
                        .WithMessage("Tactic level is required.")
                        .MaximumLength(10)
                        .WithMessage("Tactic level must be less than or equal to 10 characters.")
                        .Must(y => y.All(z => z == '*'))
                        .WithMessage("Tactic level must contain only '*' characters.");
                });
            });

        RuleFor(x => x.Tactics)
            .Must(x => x
                .GroupBy(y => new { y.Name, y.Level })
                .All(y => y.Count() == 1))
            .WithMessage("Duplicate tactics are not allowed.");
    }
}