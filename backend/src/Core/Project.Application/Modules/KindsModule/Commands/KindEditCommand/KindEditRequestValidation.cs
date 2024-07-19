using FluentValidation;
using Project.Application.Modules.KindsModule.Commands.KindAddCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.KindsModule.Commands.KindEditCommand
{
    internal class KindEditRequestValidation : AbstractValidator<KindEditRequest>
    {
        public KindEditRequestValidation()
        {
            RuleFor(m => m.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GRATHER_THAN_ONE");
        }
    }
}
