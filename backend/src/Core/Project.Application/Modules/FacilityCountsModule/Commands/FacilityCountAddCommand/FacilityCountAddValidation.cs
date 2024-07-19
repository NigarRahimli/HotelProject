

using FluentValidation;

namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountAddCommand
{
    class FacilityCountAddValidation : AbstractValidator<FacilityCountAddRequest>
    {
        public FacilityCountAddValidation()
        {
            RuleFor(x => x.PropertyId)
                .NotNull().WithErrorCode("PROPERTY_ID_CANT_BE_NULL")
                .GreaterThan(0).WithErrorCode("PROPERTY_ID_INVALID");

            RuleFor(x => x.FacilityId)
                .NotNull().WithErrorCode("FACILITY_ID_CANT_BE_NULL")
                .GreaterThan(0).WithErrorCode("FACILITY_ID_INVALID");

            RuleFor(x => x.Count)
                .NotNull().WithErrorCode("COUNT_CANT_BE_NULL")
                .GreaterThanOrEqualTo(0).WithErrorCode("COUNT_CANT_BE_NEGATIVE");
        }
    }
}
