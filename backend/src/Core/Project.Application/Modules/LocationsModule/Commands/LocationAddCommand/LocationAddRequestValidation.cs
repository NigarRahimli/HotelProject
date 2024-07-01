using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.LocationsModule.Commands.LocationAddCommand
{
    class LocationAddRequestValidation : AbstractValidator<LocationAddRequest>
    {
        public LocationAddRequestValidation()
        {
            RuleFor(m => m.Address)
                .NotNull().WithErrorCode("ADDRESS_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GRATHER_THAN_ONE");
        }
    }
}
