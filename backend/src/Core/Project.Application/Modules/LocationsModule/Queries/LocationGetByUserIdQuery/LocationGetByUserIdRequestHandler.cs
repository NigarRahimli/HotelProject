using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.LocationsModule.Queries.KindGetAllQuery;
using Project.Application.Repositories;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.LocationsModule.Queries.LocationGetByUserIdQuery
{
    class LocationGetByUserIdRequestHandler : IRequestHandler<LocationGetByUserIdRequest, IEnumerable<LocationByUserDto>>
    {
        private readonly ILocationRepository locationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<LocationGetByUserIdRequestHandler> logger;
        private readonly IMapper mapper;

        public LocationGetByUserIdRequestHandler(ILocationRepository locationRepository, IHttpContextAccessor httpContextAccessor, ILogger<LocationGetByUserIdRequestHandler> logger, IMapper mapper)
        {
            this.locationRepository = locationRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<LocationByUserDto>> Handle(LocationGetByUserIdRequest request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext!.GetUserIdExtension();
            logger.LogInformation("Handling LocationGetByUserIdRequest for UserId: {UserId}", userId);

            var entities = await locationRepository.GetAll(l => l.CreatedBy == userId && l.DeletedBy == null).ToListAsync(cancellationToken);

            var locDtos = mapper.Map<IEnumerable<LocationByUserDto>>(entities);
            logger.LogInformation("Retrieved {Count} locations for UserId: {UserId}", entities.Count, userId);
            return locDtos;
          
        }
    }
}
