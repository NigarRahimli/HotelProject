using FluentValidation;
using Project.Application.Utils;


namespace Project.Application.Modules.PropertyImagesModule.Commands.PropertyImageAddCommand
{
    class PropertyImageAddRequestValidation : AbstractValidator<PropertyImagesAddRequest>
    {
        public PropertyImageAddRequestValidation()
        {
            RuleForEach(x => x.Images)
                .Must(FileValidationUtils.BeAValidImage)
                .WithErrorCode("INVALID_IMAGE");
                
        }
    }
    }

