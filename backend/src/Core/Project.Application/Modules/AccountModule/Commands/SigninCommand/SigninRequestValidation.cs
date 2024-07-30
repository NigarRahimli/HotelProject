using FluentValidation;
using Project.Application.Modules.AccountModule.Commands.SignupCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.SigninCommand
{
     class SigninRequestValidation : AbstractValidator<SigninRequest>
    {
        public SigninRequestValidation()
        {
            RuleFor(x => x.Login)
            .NotEmpty().WithErrorCode("LOGIN_CANNOT_BE_EMPTY");     

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator());
            
        }
    }
}