using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.LocationsModule.Queries.LocationGetByIdQuery
{
    class LocationGetByIdRequestHandler : IRequestHandler<LocationGetByIdRequest, Location>
    {
        private readonly ILocationRepository locationRepository;
        private readonly ILogger<LocationGetByIdRequestHandler> logger;

        public LocationGetByIdRequestHandler(ILocationRepository locationRepository, ILogger<LocationGetByIdRequestHandler> logger)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
        }

        public async Task<Location> Handle(LocationGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling LocationGetByIdRequest for Id: {LocationId}", request.Id);
            logger.LogInformation("Retrieving location with ID {LocationId}", request.Id);
            var entity = await locationRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            logger.LogInformation("Location with Id: {LocationId} retrieved successfully", request.Id);
            return entity;
        }
    }
}
