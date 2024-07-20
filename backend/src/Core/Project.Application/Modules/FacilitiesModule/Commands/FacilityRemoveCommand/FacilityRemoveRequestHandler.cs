using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityRemoveCommand
{
    class FacilityRemoveRequestHandler : IRequestHandler<FacilityRemoveRequest>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly ILogger<FacilityRemoveRequestHandler> logger;

        public FacilityRemoveRequestHandler(IFacilityRepository facilityRepository, ILogger<FacilityRemoveRequestHandler> logger)
        {
            this.facilityRepository = facilityRepository;
            this.logger = logger;
        }

        public async Task Handle(FacilityRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityRemoveRequest for Facility Id: {FacilityId}", request.Id);

            logger.LogInformation("Retrieving Facility with ID {Id}", request.Id);
            var entity = await facilityRepository.GetAsync(x => x.Id == request.Id, cancellationToken);
            logger.LogInformation("Facility with Id: {Id} retrieved successfully", request.Id);


            logger.LogInformation("Removing Facility with Id: {FacilityId}", request.Id);
            facilityRepository.Remove(entity);

            await facilityRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Facility with Id: {FacilityId} removed successfully", request.Id);
        }
    }
}
