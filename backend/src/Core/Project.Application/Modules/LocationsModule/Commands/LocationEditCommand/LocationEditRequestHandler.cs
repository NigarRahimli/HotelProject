using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.LocationsModule.Commands.LocationEditCommand
{
    class LocationEditRequestHandler : IRequestHandler<LocationEditRequest, Location>
    {
        private readonly ILocationRepository locationRepository;
        private readonly ILogger<LocationEditRequestHandler> logger;

        public LocationEditRequestHandler(ILocationRepository locationRepository, ILogger<LocationEditRequestHandler> logger)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
        }

        public async Task<Location> Handle(LocationEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling LocationEditRequest for Location Id: {LocationId}", request.Id);

            logger.LogInformation("Retrieving location with ID {LocationId}", request.Id);
            var entity = await locationRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            logger.LogInformation("Location with Id: {LocationId} retrieved successfully", request.Id);

            entity.Longitude = request.Longitude;
            entity.Latitude = request.Latitude;
            entity.City = request.City;
            entity.Country = request.Country;
            entity.ZipCode = request.ZipCode;

            await locationRepository.SaveAsync(cancellationToken);

           logger.LogInformation("Location updated successfully for Id: {LocationId}", request.Id);

            return entity;
        }
    }
}
