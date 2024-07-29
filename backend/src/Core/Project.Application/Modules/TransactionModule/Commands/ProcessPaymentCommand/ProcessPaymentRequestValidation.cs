using FluentValidation;
using Project.Application.Utils;

namespace Project.Application.Modules.TransactionModule.Commands.ProcessPaymentCommand
{
    class ProcessPaymentRequestValidation : AbstractValidator<ProcessPaymentRequest>
    {
        public ProcessPaymentRequestValidation()
        {

            RuleFor(x => x.ReservationId)
                .GreaterThan(0).WithErrorCode("MUST_BE_GREATER_THAN_0");
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithErrorCode("MUST_BE_GREATER_THAN_0");

            RuleFor(x => x.PaymentMethod)
                .Must(TransactionValidationUtils.BeAValidTransactionMethod)
                .WithErrorCode("INVALID_PAYMENT_METHOD");

            RuleFor(x => x.Token)
                .NotEmpty().WithErrorCode("TOKEN_IS_REQUIRED");
        }
    }
}