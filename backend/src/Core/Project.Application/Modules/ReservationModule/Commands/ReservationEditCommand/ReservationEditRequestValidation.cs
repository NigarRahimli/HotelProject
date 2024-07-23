using FluentValidation;
using Project.Application.Modules.ReservationsModule.Commands.ReservationEditCommand;
using Project.Application.Utils;
using Project.Domain.Models.Enums;

namespace Project.Application.Modules.ReservationModule.Commands.ReservationEditCommand
{
    public class ReservationEditRequestValidation : AbstractValidator<ReservationEditRequest>
    {
        public ReservationEditRequestValidation()
        {
            RuleFor(m => m.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .NotEmpty().WithErrorCode("NAME_CANT_BE_EMPTY")
                .MaximumLength(100).WithErrorCode("NAME_MUST_NOT_EXCEED_100_CHARACTERS");

            RuleFor(m => m.CheckInTime)
                .NotNull().WithErrorCode("CHECKINTIME_CANT_BE_NULL")
                .GreaterThan(DateTime.Now).WithErrorCode("CHECKINTIME_MUST_BE_IN_THE_FUTURE");

            RuleFor(m => m.CheckOutTime)
                .NotNull().WithErrorCode("CHECKOUTTIME_CANT_BE_NULL")
                .GreaterThan(m => m.CheckInTime).WithErrorCode("CHECKOUTTIME_MUST_BE_AFTER_CHECKINTIME");

  
            RuleFor(x => x.PaymentOption)
                .Must(PaymentValidationUtils.BeAValidPaymentOption).WithErrorCode("INVALID_PAYMENT_OPTION");
        }


    }
}