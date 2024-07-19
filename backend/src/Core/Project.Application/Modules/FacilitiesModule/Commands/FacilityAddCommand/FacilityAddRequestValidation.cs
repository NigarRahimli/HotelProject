using FluentValidation;
using Project.Application.Modules.FacilitiesModule.Commands.FacilityAddCommand;
using Project.Application.Utils;

namespace Project.Application.Modules.FacilityModule.Commands.FacilityAddCommand
{
    class FacilityAddRequestValidation : AbstractValidator<FacilityAddRequest>
    {
        public FacilityAddRequestValidation()
        {
            RuleFor(m => m.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GRATHER_THAN_ONE")
                .MaximumLength(100).WithErrorCode("NAME_MUST_NOT_EXCEED_100_CHARACTERS");

            RuleFor(x => x.Image)
                .NotNull().WithErrorCode("IMAGE_CANT_BE_NULL")
                .Must(FileValidationUtils.BeAValidImage).WithErrorCode("INVALID_IMAGE_FORMAT");
        }
    }
}
