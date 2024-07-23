using FluentValidation;
using Project.Application.Utils;


namespace Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageEditCommand
{
    class PropertyImageEditRequestValidation : AbstractValidator<PropertyImagesEditRequest>
    {
        public PropertyImageEditRequestValidation()
        {
            RuleForEach(x => x.Images)
                .Must(FileValidationUtils.BeAValidImage)
                .WithErrorCode("INVALID_IMAGE");

        }
    }
}

