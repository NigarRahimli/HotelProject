using FluentValidation;

namespace Project.Application.Modules.AccountModule.Commands.ChangePasswordCommand
{
    class ChangePasswordRequestValidation : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidation()
        {           
            RuleFor(x => x.CurrentPassword)
                .SetValidator(new PasswordValidator());
            RuleFor(x => x.NewPassword)
                       .SetValidator(new PasswordValidator());
        }
    }
}