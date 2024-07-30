using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.SendForgotPasswordEmailCommand
{
     class SendForgotPasswordEmailRequestValidation :AbstractValidator<SendForgotPasswordEmailRequest>
    {
        public SendForgotPasswordEmailRequestValidation()
        {

         RuleFor(x => x.Email)
            .NotEmpty().WithErrorCode("EMAIL_CANNOT_BE_EMPTY")
            .EmailAddress().WithErrorCode("INVALID_EMAIL_FORMAT");


         }
    }
}