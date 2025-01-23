using FluentValidation;
using HeistHub.Application.Commands;
using HeistHub.Application.Repositories;

namespace HeistHub.Application.Validation.Validators;

public class UpdateHeistTacticsCommandValidator : AbstractValidator<UpdateHeistTacticsCommand>
{
    public UpdateHeistTacticsCommandValidator(IHeistRepository heistRepository)
    {
        RuleFor(x => x.HeistId)
            .NotEmpty().WithMessage("HeistId is required.")
            .MustAsync(async (x, _) => await heistRepository.ExistsAsync(x))
            .WithMessage("Heist with this ID not found.");

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
    }
}