using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.LocationsModule.Commands.LocationAddCommand
{
    class LocationAddRequestHandler : IRequestHandler<LocationAddRequest, Location>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;


        public LocationAddRequestHandler(ILocationRepository locationRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.locationRepository = locationRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Location> Handle(LocationAddRequest request, CancellationToken cancellationToken)
        {
            var userId=httpContextAccessor.HttpContext.GetUserIdExtension();
            var entity = new Location
            {
                Latitude= request.Latitude,
                Longitude= request.Longitude,
                Address=request.Address,
                City=request.City,
                State=request.State,
                Country=request.Country,
                ZipCode=request.ZipCode,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
            };
            await locationRepository.AddAsync(entity, cancellationToken);
            await locationRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
