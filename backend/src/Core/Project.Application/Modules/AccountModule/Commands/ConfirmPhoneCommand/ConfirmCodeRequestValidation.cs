using FluentValidation;

namespace Project.Application.Modules.AccountModule.Commands.ConfirmPhoneCommand
{
    class ConfirmCodeRequestValidation : AbstractValidator<ConfirmCodeRequest>
    {
        public ConfirmCodeRequestValidation()
        {
            RuleFor(x => x.ConfirmationCode)
                .NotEmpty().WithErrorCode("CONFIRMATION_CODE_CANNOT_BE_EMPTY")
                .Length(6).WithErrorCode("MUST_HAVE_6_DIGIT");

        }
    }
}