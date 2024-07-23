using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand
{
    public class ReviewAddRequestValidation : AbstractValidator<ReviewAddRequest>
    {
        public ReviewAddRequestValidation()
        {
            RuleFor(x => x.PropertyId)
                .GreaterThan(0).WithErrorCode("REVIEW_PROPERTY_ID_INVALID");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithErrorCode("REVIEW_CATEGORY_ID_INVALID");

            RuleFor(x => x.Stars)
                .InclusiveBetween(1, 5).WithErrorCode("REVIEW_STARS_OUT_OF_RANGE");
        }
    }
}
