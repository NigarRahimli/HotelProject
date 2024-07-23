using FluentValidation;
using Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryAddCommand;
using Project.Application.Modules.ReviewCategoriesModule.Commands.ReviewCategoryEditCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewCategoryModule.Commands.ReviewCategoryEditCommand
{
    public class ReviewCategoryEditRequestValidation : AbstractValidator<ReviewCategoryEditRequest>
    {
        public ReviewCategoryEditRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GRATHER_THAN_ONE")
                .MaximumLength(100).WithErrorCode("NAME_TOO_LONG");
        }
    }
}
