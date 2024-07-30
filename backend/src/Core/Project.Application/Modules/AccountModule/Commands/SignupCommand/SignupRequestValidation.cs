using FluentValidation;
using Project.Application.Utils;

namespace Project.Application.Modules.AccountModule.Commands.SignupCommand
{
     class SignupRequestValidation : AbstractValidator<SignupRequest>
    {
        public SignupRequestValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithErrorCode("NAME_CANNOT_BE_EMPTY");

            RuleFor(x => x.Surname)
                .NotEmpty().WithErrorCode("SURNAME_CANNOT_BE_EMPTY");

            RuleFor(x => x.Email)
                .NotEmpty().WithErrorCode("EMAIL_CANNOT_BE_EMPTY")
                .EmailAddress().WithErrorCode("INVALID_EMAIL_FORMAT");

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator());
            RuleFor(x => x.ConfirmPassword)
                       .Equal(x => x.Password).WithErrorCode("PASSWORDS_DO_NOT_MATCH");
        }
    }
}