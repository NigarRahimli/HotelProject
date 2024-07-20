using FluentValidation;

namespace Project.Application.Modules.LocationsModule.Commands.LocationEditCommand
{
    public class LocationEditRequestValidation : AbstractValidator<LocationEditRequest>
    {
        public LocationEditRequestValidation()
        {
            RuleFor(m => m.Latitude)
                .NotEmpty().WithErrorCode("LATTITUDE_CANT_BE_EMPTY")
                .InclusiveBetween(-90, 90).WithErrorCode("LATITUDE_OUT_OF_RANGE");


            RuleFor(m => m.Longitude)
                .NotEmpty().WithErrorCode("LONGITUDE_CANT_BE_EMPTY")
                .InclusiveBetween(-180, 180).WithErrorCode("LONGITUDE_OUT_OF_RANGE");

            RuleFor(m => m.Address)
                .NotNull().WithErrorCode("EditRESS_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("EditRESS_MINLENGTH_GRATHER_THAN_ONE");

            RuleFor(m => m.City)
                .NotNull().WithErrorCode("CITY_CANT_BE_NULL")
                .MinimumLength(2).WithErrorCode("City must be at least 2 characters long.")
                .WithErrorCode("CITY_MINLENGTH_GRATHER_THAN_ONE");

            RuleFor(m => m.Country)
                .NotNull().WithErrorCode("COUNTRY_CANT_BE_NULL.")
                .MinimumLength(2).WithErrorCode("COUNTRY_MINLENGTH_GRATHER_THAN_ONE");

            RuleFor(m => m.ZipCode)
                .NotNull().WithErrorCode("ZIPCODE_CANT_BE_NULL")
                .Matches(@"^\d{5}(?:[-\s]\d{4})?$").WithErrorCode("ZIPCODE_INVALID_FORMAT");
        }
    }
}
