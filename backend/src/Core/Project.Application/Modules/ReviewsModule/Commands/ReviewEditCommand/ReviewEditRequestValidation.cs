using FluentValidation;
using Project.Application.Modules.Module.Commands.EditCommand;
using Project.Application.Modules.ReviewsModule.Commands.ReviewAddCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.ReviewsModule.Commands.ReviewEditCommand
{
    public class ReviewEditRequestValidation : AbstractValidator<ReviewEditRequest>
    {
        public ReviewEditRequestValidation()
        {
         
            RuleFor(x => x.Stars)
                .InclusiveBetween(1, 5).WithErrorCode("REVIEW_STARS_OUT_OF_RANGE");
        }
    }
}
