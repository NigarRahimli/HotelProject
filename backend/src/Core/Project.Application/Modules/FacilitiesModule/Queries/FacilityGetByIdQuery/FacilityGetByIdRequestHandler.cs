using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByIdQuery
{
    class FacilityGetByIdRequestHandler : IRequestHandler<FacilityGetByIdRequest, Facility>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly ILogger<FacilityGetByIdRequestHandler> logger;

        public FacilityGetByIdRequestHandler(IFacilityRepository facilityRepository, ILogger<FacilityGetByIdRequestHandler> logger)
        {
            this.facilityRepository = facilityRepository;
            this.logger = logger;
        }

        public async Task<Facility> Handle(FacilityGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityGetByIdRequest for Facility Id: {FacilityId}", request.Id);

            var entity = await facilityRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);

            if (entity == null)
            {
                logger.LogWarning("Facility with Id: {FacilityId} not found or deleted", request.Id);
                throw new Exception($"Facility with Id: {request.Id} not found or deleted");
            }

            logger.LogInformation("Facility with Id: {FacilityId} retrieved successfully", request.Id);
            return entity;
        }
    }
}
