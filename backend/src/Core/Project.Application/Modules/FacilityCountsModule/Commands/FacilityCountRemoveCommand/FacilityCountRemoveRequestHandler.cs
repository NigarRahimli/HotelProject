using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountRemoveCommand
{
    class FacilityCountRemoveRequestHandler : IRequestHandler<FacilityCountRemoveRequest>
    {
        private readonly IFacilityCountRepository facilityCountRepository;
        private readonly ILogger<FacilityCountRemoveRequestHandler> logger;

        public FacilityCountRemoveRequestHandler(IFacilityCountRepository facilityCountRepository, ILogger<FacilityCountRemoveRequestHandler> logger)
        {
            this.facilityCountRepository = facilityCountRepository;
            this.logger = logger;
        }

        public async Task Handle(FacilityCountRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityCountRemoveRequest for Id: {Id}", request.Id);

            logger.LogInformation("Retrieving kind with ID {KindId}", request.Id);
            var entity = await facilityCountRepository.GetAsync(x => x.Id == request.Id, cancellationToken);
            logger.LogInformation("Kind with Id: {KindId} retrieved successfully", request.Id);


            logger.LogInformation("Removing FacilityCount Id: {Id}", request.Id);
            facilityCountRepository.Remove(entity);

            await facilityCountRepository.SaveAsync(cancellationToken);
            logger.LogInformation("FacilityCount with Id: {Id} removed successfully", request.Id);
        }
    }
}