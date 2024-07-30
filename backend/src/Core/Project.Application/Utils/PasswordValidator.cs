using FluentValidation;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty().WithErrorCode("PASSWORD_CANNOT_BE_EMPTY")
            .MinimumLength(8).WithErrorCode("PASSWORD_TOO_SHORT")
            .Matches(@"[A-Z]").WithErrorCode("PASSWORD_NO_UPPERCASE")
            .Matches(@"[a-z]").WithErrorCode("PASSWORD_NO_LOWERCASE")
            .Matches(@"\d").WithErrorCode("PASSWORD_NO_NUMBER")
            .Matches(@"[!@#$%^&*(),.?""\:{}\|<>]").WithErrorCode("PASSWORD_NO_SPECIAL_CHAR");
    }
}
