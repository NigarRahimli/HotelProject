using FluentValidation;
using Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllNearbyQuery
{
    class PropertyGetAllNearbyRequestValidation : AbstractValidator<PropertyGetAllNearbyRequest>
    {
        public PropertyGetAllNearbyRequestValidation()
        {
            RuleFor(m => m.Latitude)
              .NotEmpty().WithErrorCode("LATTITUDE_CANT_BE_EMPTY")
              .InclusiveBetween(-90, 90).WithErrorCode("LATITUDE_OUT_OF_RANGE");


            RuleFor(m => m.Longitude)
                .NotEmpty().WithErrorCode("LONGITUDE_CANT_BE_EMPTY")
                .InclusiveBetween(-180, 180).WithErrorCode("LONGITUDE_OUT_OF_RANGE");

        }
    }
}
