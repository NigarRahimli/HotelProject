using FluentValidation;
using Project.Application.Utils;

namespace Project.Application.Modules.AccountModule.Commands.EditProfilePhotoCommand
{
    class EditProfilePhotoRequestValidation : AbstractValidator<EditProfilePhotoRequest>
    {
        public EditProfilePhotoRequestValidation()
        {
            RuleFor(x => x.ProfileImg)
                .NotNull().WithErrorCode("IMAGE_CANT_BE_NULL")
                .Must(FileValidationUtils.BeAValidImage).WithErrorCode("INVALID_IMAGE_FORMAT");
        }
    }
}

