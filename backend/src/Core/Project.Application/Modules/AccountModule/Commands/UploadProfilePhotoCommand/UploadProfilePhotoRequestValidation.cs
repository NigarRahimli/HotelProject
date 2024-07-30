using FluentValidation;
using Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand;
using Project.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Modules.AccountModule.Commands.UploadProfilePhotoCommand
{
     class UploadProfilePhotoRequestValidation : AbstractValidator<UploadProfilePhotoRequest>
    {
        public UploadProfilePhotoRequestValidation()
        {
            RuleFor(x => x.ProfileImg)
                .NotNull().WithErrorCode("IMAGE_CANT_BE_NULL")
                .Must(FileValidationUtils.BeAValidImage).WithErrorCode("INVALID_IMAGE_FORMAT");
        }
    }
}
