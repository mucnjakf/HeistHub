using FluentValidation;
using HeistHub.Application.Commands;

namespace HeistHub.Application.Validation.Validators;

public sealed class UpdateMemberSkillsCommandValidator : AbstractValidator<UpdateMemberSkillsCommand>
{
    public UpdateMemberSkillsCommandValidator()
    {
        RuleFor(x => x.MemberId)
            .NotEmpty().WithMessage("MemberId is required.");
    }
}