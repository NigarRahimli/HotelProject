
using FluentValidation;
using Project.Application.Modules.RoleModule.Commands.RoleAddCommand;

namespace Project.Application.Modules.RoleModule.Commands.RoleEditCommand
{
     class RoleEditRequestValidation : AbstractValidator<RoleEditRequest>
    {
        public RoleEditRequestValidation()
        {

            RuleFor(m => m.Name)
                .NotNull().WithErrorCode("NAME_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("NAME_MINLENGTH_GRATHER_THAN_ONE")
                .MaximumLength(100).WithErrorCode("NAME_MUST_NOT_EXCEED_100_CHARACTERS");
        }

    }
}
