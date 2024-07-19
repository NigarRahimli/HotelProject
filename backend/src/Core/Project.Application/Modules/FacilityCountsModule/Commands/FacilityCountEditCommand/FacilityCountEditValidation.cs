using FluentValidation;
using Project.Application.Modules.DescriptionsModule.Commands.DescriptionAddCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountEditCommand
{
     class FacilityCountEditValidation : AbstractValidator<FacilityCountEditRequest>
    {
        public FacilityCountEditValidation()
        {
            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0).WithErrorCode("COUNT_CANT_BE_NEGATIVE");
        }
    }
}
