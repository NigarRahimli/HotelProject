using FluentValidation;
using Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryAddCommand;

namespace Project.Application.Modules.ReviewCategoryModule.Commands.ReviewCategoryAddCommand
{
    public class ReviewCategoryAddRequestValidator : AbstractValidator<ReviewCategoryAddRequest>
    {
        public ReviewCategoryAddRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GRATHER_THAN_ONE")
                .MaximumLength(100).WithErrorCode("NAME_TOO_LONG");
        }
    }
}
