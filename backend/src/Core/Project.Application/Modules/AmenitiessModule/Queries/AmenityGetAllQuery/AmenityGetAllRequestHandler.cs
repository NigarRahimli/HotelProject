using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.AmenitiessModule.Queries;
using Project.Application.Repositories;

namespace Project.Application.Modules.AmenitiesModule.Queries.AmenityGetAllQuery
{
    class AmenityGetAllRequestHandler : IRequestHandler<AmenityGetAllRequest, IEnumerable<AmenityDto>>
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AmenityGetAllRequestHandler> logger;

        public AmenityGetAllRequestHandler(IAmenityRepository amenityRepository, IMapper mapper, ILogger<AmenityGetAllRequestHandler> logger)
        {
            this.amenityRepository = amenityRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IEnumerable<AmenityDto>> Handle(AmenityGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling AmenityGetAllRequest");

            var entities = await amenityRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            if (entities == null || !entities.Any())
            {
                logger.LogWarning("No amenities found");
                return Enumerable.Empty<AmenityDto>();
            }

            logger.LogInformation("Mapping amenities to AmenityDto");
            var amenityDtos = mapper.Map<IEnumerable<AmenityDto>>(entities);
            logger.LogInformation("Successfully handled AmenityGetAllRequest");

            return amenityDtos;
        }
    }
}
