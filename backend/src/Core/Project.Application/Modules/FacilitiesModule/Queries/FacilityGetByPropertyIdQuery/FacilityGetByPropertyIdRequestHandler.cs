using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Modules.FacilitiesModule.Queries;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByPropertyIdQuery;
using Project.Application.Repositories;

namespace Project.Application.Modules.FacilitiesModule.Handlers
{
    public class FacilityGetByPropertyIdRequestHandler : IRequestHandler<FacilityGetByPropertyIdRequest, IEnumerable<FacilityDetailDto>>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<FacilityGetByPropertyIdRequestHandler> logger;

        public FacilityGetByPropertyIdRequestHandler(IFacilityRepository facilityRepository, IPropertyRepository propertyRepository, ILogger<FacilityGetByPropertyIdRequestHandler> logger)
        {
            this.facilityRepository = facilityRepository;
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<FacilityDetailDto>> Handle(FacilityGetByPropertyIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityGetByPropertyIdRequest for Property Id: {PropertyId}", request.PropertyId);

            var property = await propertyRepository.GetAsync(m => m.Id == request.PropertyId, cancellationToken);
            if (property == null)
            {
                logger.LogWarning("Property with Id: {PropertyId} not found", request.PropertyId);
                throw new Exception($"Property with Id: {request.PropertyId} not found");
            }

            logger.LogInformation("Retrieving facilities for Property Id: {PropertyId}", request.PropertyId);
            var facilityDtos = await facilityRepository.GetFacilitiesByPropertyIdAsync(request.PropertyId, cancellationToken);

            if (facilityDtos == null || !facilityDtos.Any())
            {
                logger.LogWarning("No facilities found for Property Id: {PropertyId}", request.PropertyId);
            }
            else
            {
                logger.LogInformation("{FacilityCount} facilities retrieved for Property Id: {PropertyId}", facilityDtos.Count(), request.PropertyId);
            }

            return facilityDtos;
        }
    }
}
