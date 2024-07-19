using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetAllQuery
{
    class FacilityGetAllRequestHandler : IRequestHandler<FacilityGetAllRequest, IEnumerable<FacilityAllDto>>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly ILogger<FacilityGetAllRequestHandler> logger;
        private readonly IMapper mapper;

        public FacilityGetAllRequestHandler(IFacilityRepository facilityRepository, ILogger<FacilityGetAllRequestHandler> logger, IMapper mapper)
        {
            this.facilityRepository = facilityRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FacilityAllDto>> Handle(FacilityGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityGetAllRequest");

            var entities = await facilityRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);

            if (entities == null || !entities.Any())
            {
                logger.LogWarning("No facilities found or all facilities are deleted");
            }
            else
            {
                logger.LogInformation("{FacilityCount} facilities retrieved", entities.Count());
            }
            var facilityDtos = mapper.Map<IEnumerable<FacilityAllDto>>(entities);
            return facilityDtos;
        }
    }
}
