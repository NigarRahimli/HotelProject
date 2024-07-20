using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountEditCommand
{
    class FacilityCountEditRequestHandler : IRequestHandler<FacilityCountEditRequest, FacilityCount>
    {
        private readonly IFacilityCountRepository facilityCountRepository;
        private readonly ILogger<FacilityCountEditRequestHandler> logger;

        public FacilityCountEditRequestHandler(IFacilityCountRepository facilityCountRepository, ILogger<FacilityCountEditRequestHandler> logger)
        {
            this.facilityCountRepository = facilityCountRepository;
            this.logger = logger;
        }

        public async Task<FacilityCount> Handle(FacilityCountEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityCountEditRequest for Id: {Id}", request.Id);

            logger.LogInformation("Retrieving FacilityCount with ID {Id}", request.Id);
            var entity = await facilityCountRepository.GetAsync(x => x.Id == request.Id, cancellationToken);
            logger.LogInformation("FacilityCount with Id: {Id} retrieved successfully", request.Id);

            logger.LogInformation("Updating count for FacilityCount Id: {Id}", request.Id);
            entity.Count = request.Count;

            await facilityCountRepository.SaveAsync(cancellationToken);
            logger.LogInformation("FacilityCount with Id: {Id} updated successfully", request.Id);

            return entity;
        }
    }
}
