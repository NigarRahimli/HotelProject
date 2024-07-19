using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Abstracts;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand
{
    class AmenityAddRequestHandler : IRequestHandler<AmenityAddRequest, Amenity>
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly IFileService fileService;
        private readonly ILogger<AmenityAddRequestHandler> logger;

        public AmenityAddRequestHandler(IAmenityRepository amenityRepository, IFileService fileService, ILogger<AmenityAddRequestHandler> logger)
        {
            this.amenityRepository = amenityRepository;
            this.fileService = fileService;
            this.logger = logger;
        }

        public async Task<Amenity> Handle(AmenityAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling AmenityAddRequest");

            var entity = new Amenity
            {
                Name = request.Name
            };

            if (request.Image != null)
            {
                logger.LogInformation("Uploading image for new Amenity");
                var file = await fileService.UploadSingleAsync(request.Image);
                entity.IconUrl = file.Url.ToString();
                logger.LogInformation("Image uploaded successfully for new Amenity");
            }

            await amenityRepository.AddAsync(entity, cancellationToken);
            logger.LogInformation("Amenity added to repository");

            await amenityRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Amenity saved successfully");

            return entity;
        }
    }
}
