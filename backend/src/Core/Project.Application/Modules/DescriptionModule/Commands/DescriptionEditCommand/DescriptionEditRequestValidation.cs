using FluentValidation;
using Project.Application.Modules.DescriptionsModule.Commands.DescriptionEditCommand;

namespace Project.Application.Modules.DescriptionModule.Commands.DescriptionEditCommand
{
    class DescriptionEditRequestValidation : AbstractValidator<DescriptionEditRequest>
    {
        public DescriptionEditRequestValidation()
        {
            RuleFor(m => m.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GRATHER_THAN_ONE")
                .MaximumLength(100).WithErrorCode("NAME_MUST_NOT_EXCEED_100_CHARACTERS");

            RuleFor(m => m.Explanation)
             .MaximumLength(200).WithErrorCode("NAME_MUST_NOT_EXCEED_200_CHARACTERS");

        }
    }
}
