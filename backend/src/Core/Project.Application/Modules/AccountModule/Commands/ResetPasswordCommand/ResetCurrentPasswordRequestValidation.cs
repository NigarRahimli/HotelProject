using FluentValidation;

namespace Project.Application.Modules.AccountModule.Commands.ResetPasswordCommand
{
    class ResetCurrentPasswordRequestValidation : AbstractValidator<ResetCurrentPasswordRequest>
    {
        public ResetCurrentPasswordRequestValidation()
        {

            RuleFor(x => x.Email)
               .NotEmpty().WithErrorCode("EMAIL_CANNOT_BE_EMPTY");

            RuleFor(x => x.Token)
              .NotEmpty().WithErrorCode("TOKEN_CANNOT_BE_EMPTY");
      
            RuleFor(x => x.NewPassword)
                .SetValidator(new PasswordValidator());


        }
    }
}