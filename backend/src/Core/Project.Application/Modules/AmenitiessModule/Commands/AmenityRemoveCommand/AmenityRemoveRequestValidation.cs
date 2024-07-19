using FluentValidation;
using Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand;
using Project.Application.Modules.AmenitiesModule.Commands.AmenityRemoveCommand;
using Project.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiessModule.Commands.AmenityRemoveCommand
{
     class AmenityRemoveRequestValidation : AbstractValidator<AmenityRemoveRequest>
    {
        public AmenityRemoveRequestValidation()
        {

            RuleFor(m => m.Id)
                .GreaterThan(0).WithErrorCode("ID_MUST_BE_GREATER_THAN_0")
                .NotEmpty().WithErrorCode("ID_CANT_BE_NULL");


        }
    }
}
