using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountAddCommand
{
    class FacilityCountAddRequestHandler : IRequestHandler<FacilityCountAddRequest, FacilityCount>
    {
        private readonly IFacilityCountRepository facilityCountRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IFacilityRepository facilityRepository;
        private readonly ILogger<FacilityCountAddRequestHandler> logger;

        public FacilityCountAddRequestHandler(
            IFacilityCountRepository facilityCountRepository,
            IPropertyRepository propertyRepository,
            IFacilityRepository facilityRepository,
            ILogger<FacilityCountAddRequestHandler> logger)
        {
            this.facilityCountRepository = facilityCountRepository;
            this.propertyRepository = propertyRepository;
            this.facilityRepository = facilityRepository;
            this.logger = logger;
        }

        public async Task<FacilityCount> Handle(FacilityCountAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityCountAddRequest for Property Id: {PropertyId} and Facility Id: {FacilityId}", request.PropertyId, request.FacilityId);

            var property = await propertyRepository.GetAsync(x => x.Id == request.PropertyId);
            if (property == null)
            {
                logger.LogWarning("Property with Id: {PropertyId} not found", request.PropertyId);
                throw new Exception($"Property with Id: {request.PropertyId} not found");
            }

            var facility = await facilityRepository.GetAsync(x => x.Id == request.FacilityId);
            if (facility == null)
            {
                logger.LogWarning("Facility with Id: {FacilityId} not found", request.FacilityId);
                throw new Exception($"Facility with Id: {request.FacilityId} not found");
            }

            logger.LogInformation("Creating new FacilityCount for Property Id: {PropertyId} and Facility Id: {FacilityId}", request.PropertyId, request.FacilityId);
            var entity = new FacilityCount
            {
                PropertyId = request.PropertyId,
                FacilityId = request.FacilityId,
                Count = request.Count,
            };

            await facilityCountRepository.AddAsync(entity, cancellationToken);
            await facilityCountRepository.SaveAsync(cancellationToken);

            logger.LogInformation("FacilityCount created successfully for Property Id: {PropertyId} and Facility Id: {FacilityId}", request.PropertyId, request.FacilityId);

            return entity;
        }
    }
}
