using FluentValidation;
using Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyPagedQuery
{
    class PropertyPagedRequestValidation : AbstractValidator<PropertyPagedRequest>
    {
        public PropertyPagedRequestValidation()
        {
            RuleFor(m => m.CheckInTime)
                .LessThanOrEqualTo(m => m.CheckOutTime)
                .When(m => m.CheckInTime.HasValue && m.CheckOutTime.HasValue)
                .WithErrorCode("CHECKINTIME_MUST_BE_BEFORE_CHECKOUTTIME");

            RuleFor(m => m.GuestNum)
                .GreaterThan(0).When(m => m.GuestNum.HasValue)
                .WithErrorCode("GUESTNUM_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(m => m.KindId)
                .GreaterThan(0).When(m => m.KindId.HasValue)
                .WithErrorCode("KINDID_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(m => m.CityName)
                .MaximumLength(100).WithErrorCode("CITYNAME_MUST_NOT_EXCEED_100_CHARACTERS")
                .NotEmpty().When(m => !string.IsNullOrWhiteSpace(m.CityName))
                .WithErrorCode("CITYNAME_CANT_BE_EMPTY_WHEN_PROVIDED");
        }
    }
}
