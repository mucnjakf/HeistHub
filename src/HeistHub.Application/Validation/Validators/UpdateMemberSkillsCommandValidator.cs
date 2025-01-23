using FluentValidation;
using HeistHub.Application.Commands;
using HeistHub.Application.Repositories;

namespace HeistHub.Application.Validation.Validators;

public sealed class UpdateMemberSkillsCommandValidator : AbstractValidator<UpdateMemberSkillsCommand>
{
    public UpdateMemberSkillsCommandValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.MemberId)
            .NotEmpty().WithMessage("MemberId is required.")
            .MustAsync(async (x, _) => await memberRepository.ExistsAsync(x))
            .WithMessage("Member with this ID not found.");
    }
}