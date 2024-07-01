using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.LocationsModule.Queries.LocationGetAllQuery
{
    class LocationGetAllRequestHandler : IRequestHandler<LocationGetAllRequest, IEnumerable<Location>>
    {
        private readonly ILocationRepository LocationRepository;

        public LocationGetAllRequestHandler(ILocationRepository LocationRepository)
        {
            this.LocationRepository = LocationRepository;
        }
        public async Task<IEnumerable<Location>> Handle(LocationGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await LocationRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
