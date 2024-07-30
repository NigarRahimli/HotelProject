using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.SendPhoneConfirmationCommand
{
    internal class SendPhoneConfirmationRequestValidation : AbstractValidator<SendPhoneConfirmationRequest>
    {
        public SendPhoneConfirmationRequestValidation()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number cannot be empty.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithErrorCode("INVALID_PHONE_NUMBER_FORMAT");
        }
    }
}