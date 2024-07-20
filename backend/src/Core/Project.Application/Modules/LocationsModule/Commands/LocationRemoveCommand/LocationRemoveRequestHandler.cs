using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.LocationsModule.Commands.LocationRemoveCommand
{
    class LocationRemoveRequestHandler : IRequestHandler<LocationRemoveRequest>
    {
        private readonly ILocationRepository locationRepository;
        private readonly ILogger<LocationRemoveRequestHandler> logger;

        public LocationRemoveRequestHandler(ILocationRepository locationRepository, ILogger<LocationRemoveRequestHandler> logger)
        {
            this.locationRepository = locationRepository;
            this. logger = logger;
        }

        public async Task Handle(LocationRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling LocationRemoveRequest for Location Id: {LocationId}", request.Id);
            logger.LogInformation("Retrieving location with ID {LocationId}", request.Id);
            var entity = await locationRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            logger.LogInformation("Location with Id: {LocationId} retrieved successfully", request.Id);

            locationRepository.Remove(entity);
            await locationRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Location removed successfully for Id: {LocationId}", request.Id);
        }
    }
}
