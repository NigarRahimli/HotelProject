using FluentValidation;


namespace Project.Application.Modules.RoleModule.Commands.ChangeAccessCommand
{
    public class ChangeAccessRequestValidation : AbstractValidator<ChangeAccessRequest>
    {
        public ChangeAccessRequestValidation()
        {
            RuleFor(request => request.PolicyName)
                .NotEmpty().WithErrorCode("POLICY_NAME_CANT_BE_EMPTY")
                .MaximumLength(100).WithErrorCode("POLICY_NAME_MAX_LENGTH_100");

            //RuleFor(request => request.RoleId)
            //    .GreaterThan(0).WithErrorCode("ROLE_ID_MUST_BE_GREATER_THAN_ZERO");

            RuleFor(request => request.Policies)
                .NotNull().WithErrorCode("POLICIES_CANT_BE_NULL");

            RuleForEach(request => request.Policies)
                .NotEmpty().WithErrorCode("POLICY_CANT_BE_EMPTY")
                .MaximumLength(100).WithErrorCode("POLICY_MAX_LENGTH_100");
        }
    }
}