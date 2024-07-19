using FluentValidation;
using Project.Application.Modules.DescriptionsModule.Commands.DescriptionAddCommand;
using Project.Application.Utils;

namespace Project.Application.Modules.DescriptionModule.Commands.DescriptionAddCommand
{
    class DescriptionAddRequestValidation : AbstractValidator<DescriptionAddRequest>
    {
        public DescriptionAddRequestValidation()
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
