using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.LocationsModule.Queries.LocationGetByIdQuery
{
    class LocationGetByIdRequestHandler : IRequestHandler<LocationGetByIdRequest, Location>
    {
        private readonly ILocationRepository LocationRepository;

        public LocationGetByIdRequestHandler(ILocationRepository LocationRepository)
        {
            this.LocationRepository = LocationRepository;
        }
        public async Task<Location> Handle(LocationGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await LocationRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
