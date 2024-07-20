using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.LocationsModule.Queries.LocationGetAllQuery
{
    class LocationGetAllRequestHandler : IRequestHandler<LocationGetAllRequest, IEnumerable<Location>>
    {
        private readonly ILocationRepository locationRepository;
        private readonly ILogger<LocationGetAllRequestHandler> logger;

        public LocationGetAllRequestHandler(ILocationRepository locationRepository, ILogger<LocationGetAllRequestHandler> logger)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<Location>> Handle(LocationGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling LocationGetAllRequest");

            var entities = await locationRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);

            logger.LogInformation("Retrieved {LocationCount} locations", entities.Count);
             
            return entities;
        }
    }
}
