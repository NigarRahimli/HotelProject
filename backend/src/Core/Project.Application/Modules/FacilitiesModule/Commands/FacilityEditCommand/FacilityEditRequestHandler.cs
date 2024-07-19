using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityEditCommand
{
    class FacilityEditRequestHandler : IRequestHandler<FacilityEditRequest, Facility>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly IFileService fileService;
        private readonly ILogger<FacilityEditRequestHandler> logger;

        public FacilityEditRequestHandler(IFacilityRepository facilityRepository, IFileService fileService, ILogger<FacilityEditRequestHandler> logger)
        {
            this.facilityRepository = facilityRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Facility> Handle(FacilityEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityEditRequest for Facility Id: {FacilityId}", request.Id);

            var entity = await facilityRepository.GetAsync(m => m.Id == request.Id && m.DeletedBy == null, cancellationToken);
            if (entity == null)
            {
                logger.LogWarning("Facility with Id: {FacilityId} not found", request.Id);
            }

            logger.LogInformation("Updating Facility with Id: {FacilityId}", request.Id);
            entity.Name = request.Name;

            if (request.Image != null)
            {
                logger.LogInformation("Updating Facility icon for Facility Id: {FacilityId}", request.Id);
                var icon = await fileService.ChangeSingleFileAsync(entity.IconUrl, request.Image);
                entity.IconUrl = icon.Url;
            }

            await facilityRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Facility with Id: {FacilityId} updated successfully", request.Id);

            return entity;
        }
    }
}
