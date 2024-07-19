using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityAddCommand
{
    class FacilityAddRequestHandler : IRequestHandler<FacilityAddRequest, Facility>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly IFileService fileService;
        private readonly ILogger<FacilityAddRequestHandler> logger;

        public FacilityAddRequestHandler(IFacilityRepository facilityRepository, IFileService fileService, ILogger<FacilityAddRequestHandler> logger)
        {
            this.facilityRepository = facilityRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Facility> Handle(FacilityAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling FacilityAddRequest");

            var entity = new Facility
            {
                Name = request.Name
            };

            logger.LogInformation("Uploading facility icon");
            var icon = await fileService.UploadSingleAsync(request.Image, "icons");
            entity.IconUrl = icon.Url;

            logger.LogInformation("Adding new Facility with Name: {FacilityName}", request.Name);
            await facilityRepository.AddAsync(entity, cancellationToken);
            logger.LogInformation("Facility added to repository");

            await facilityRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Facility saved successfully");

            return entity;
        }
    }
}
