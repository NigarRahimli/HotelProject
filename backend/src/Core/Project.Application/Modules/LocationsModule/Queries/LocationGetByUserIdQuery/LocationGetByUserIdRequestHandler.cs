using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Application.Modules.LocationsModule.Queries.KindGetAllQuery;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Extensions;


namespace Project.Application.Modules.LocationsModule.Queries.LocationGetAllQuery
{
    class LocationGetByUserIdRequestHandler : IRequestHandler<LocationGetByUserIdRequest, IEnumerable<Location>>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocationGetByUserIdRequestHandler(ILocationRepository locationRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.locationRepository = locationRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Location>> Handle(LocationGetByUserIdRequest request, CancellationToken cancellationToken)
        {
            var userId =  httpContextAccessor.HttpContext!.GetUserIdExtension();
            var entities = await locationRepository.GetAll(l =>l.CreatedBy== userId && l.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
