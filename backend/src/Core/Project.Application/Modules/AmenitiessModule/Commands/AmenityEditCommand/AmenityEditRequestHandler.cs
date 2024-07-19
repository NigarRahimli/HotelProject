using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityEditCommand
{
    class AmenityEditRequestHandler : IRequestHandler<AmenityEditRequest, Amenity>
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly IFileService fileService;
        private readonly ILogger<AmenityEditRequestHandler> logger;

        public AmenityEditRequestHandler(IAmenityRepository amenityRepository, IFileService fileService, ILogger<AmenityEditRequestHandler> logger)
        {
            this.amenityRepository = amenityRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Amenity> Handle(AmenityEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling AmenityEditRequest for Amenity Id: {AmenityId}", request.Id);

            var entity = await amenityRepository.GetAsync(m => m.Id == request.Id && m.DeletedBy == null);
            if (entity == null)
            {
                logger.LogWarning("Amenity with Id: {AmenityId} not found or deleted", request.Id);
            }

            entity.Name = request.Name;
            if (request.Image != null)
            {
                logger.LogInformation("Updating image for Amenity Id: {AmenityId}", request.Id);
                var imageName = await fileService.ChangeSingleFileAsync(entity.IconUrl, request.Image);
                logger.LogInformation("Image updated successfully for Amenity Id: {AmenityId}", request.Id);
            }

            await amenityRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Amenity with Id: {AmenityId} updated successfully", request.Id);

            return entity;
        }
    }
}
