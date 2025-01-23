using FluentValidation;
using HeistHub.Application.Commands;
using HeistHub.Application.Repositories;

namespace HeistHub.Application.Validation.Validators;

public sealed class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.")
            .MustAsync(async (email, _) => !await memberRepository.EmailExistsAsync(email))
            .WithMessage("A member with this email already exists.");

        RuleFor(x => x.Skills)
            .NotEmpty()
            .WithMessage("At least one skill is required.")
            .ForEach(x =>
            {
                x.ChildRules(skillValidator =>
                {
                    skillValidator.RuleFor(y => y.Name)
                        .NotEmpty()
                        .WithMessage("Skill name is required.");

                    skillValidator.RuleFor(y => y.Level)
                        .NotEmpty()
                        .WithMessage("Skill level is required.")
                        .MaximumLength(10)
                        .WithMessage("Skill level must be less than or equal to 10 characters.")
                        .Must(y => y.All(z => z == '*'))
                        .WithMessage("Skill level must contain only '*' characters.");
                });
            });

        RuleFor(x => x.Skills)
            .Must(x => x.GroupBy(y => y.Name).Any(y => y.Count() == 1))
            .WithMessage("Duplicate skill name are not allowed.");
    }
}