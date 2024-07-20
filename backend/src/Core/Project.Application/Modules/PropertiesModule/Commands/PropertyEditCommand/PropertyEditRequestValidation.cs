using FluentValidation;
using Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand;

namespace Project.Application.Modules.AmenitiessModule.Commands.PropertyEditCommand
{
    class PropertyEditRequestValidation : AbstractValidator<PropertyEditRequest>
    {
        public PropertyEditRequestValidation()
        {
            RuleFor(m => m.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GREATER_THAN_ONE")
                .MaximumLength(100).WithErrorCode("NAME_MUST_NOT_EXCEED_100_CHARACTERS");

            RuleFor(m => m.GuestNum)
                .GreaterThan(0).WithErrorCode("GUESTNUM_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(m => m.DescriptionId)
                .GreaterThan(0).WithErrorCode("DESCRIPTIONID_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(m => m.KindId)
                .GreaterThan(0).WithErrorCode("KINDID_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(m => m.LocationId)
                .GreaterThan(0).WithErrorCode("LOCATIONID_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(m => m.LongPrice)
                .GreaterThanOrEqualTo(0).WithErrorCode("LONGPRICE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_ZERO");

            RuleFor(m => m.MedPrice)
                .GreaterThanOrEqualTo(0).WithErrorCode("MEDPRICE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_ZERO");

            RuleFor(m => m.MinPrice)
                .GreaterThanOrEqualTo(0).WithErrorCode("MINPRICE_MUST_BE_GREATER_THAN_OR_EQUAL_TO_ZERO");

            RuleFor(m => m.MinPrice)
                .LessThanOrEqualTo(m => m.MedPrice)
                .WithErrorCode("MINPRICE_MUST_BE_LESS_THAN_OR_EQUAL_TO_MEDPRICE");

            RuleFor(m => m.MedPrice)
                .LessThanOrEqualTo(m => m.LongPrice)
                .WithErrorCode("MEDPRICE_MUST_BE_LESS_THAN_OR_EQUAL_TO_LONGPRICE");

            RuleFor(m => m.IsPetFriendly)
                .Must(x => x == true || x == false).WithErrorCode("ISPETFRIENDLY_MUST_BE_BOOLEAN");

        }
    }
}
