using FluentValidation;

namespace Project.Application.Modules.RoleModule.Commands.ManageMemberCommand
{
    public class ManageMemberRequestValidation : AbstractValidator<ManageMemberRequest>
    {
        public ManageMemberRequestValidation()
        {
            //RuleFor(request => request.MemberId)
            //    .GreaterThan(0).WithErrorCode("MEMBER_ID_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(request => request.RoleId)
                .GreaterThan(0).WithErrorCode("ROLE_ID_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(request => request.IsSelected)
                .NotNull().WithErrorCode("IS_SELECTED_CANT_BE_NULL");
        }
    }
}