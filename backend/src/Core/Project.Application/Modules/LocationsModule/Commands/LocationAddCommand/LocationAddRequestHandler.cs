using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.LocationsModule.Commands.LocationAddCommand
{
    class LocationAddRequestHandler : IRequestHandler<LocationAddRequest, Location>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<LocationAddRequestHandler> logger;

        public LocationAddRequestHandler(ILocationRepository locationRepository, IHttpContextAccessor httpContextAccessor, ILogger<LocationAddRequestHandler> logger)
        {
            this.locationRepository = locationRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<Location> Handle(LocationAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling LocationAddRequest for Address: {Address}", request.Address);

            var entity = new Location
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                ZipCode = request.ZipCode,
            };

            await locationRepository.AddAsync(entity, cancellationToken);
            await locationRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Location added successfully with Id: {LocationId}", entity.Id);

            return entity;
        }
    }
}
