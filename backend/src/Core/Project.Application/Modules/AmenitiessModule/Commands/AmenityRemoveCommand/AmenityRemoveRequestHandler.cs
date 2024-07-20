using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityRemoveCommand
{
    class AmenityRemoveRequestHandler : IRequestHandler<AmenityRemoveRequest>
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly ILogger<AmenityRemoveRequestHandler> logger;

        public AmenityRemoveRequestHandler(IAmenityRepository amenityRepository, ILogger<AmenityRemoveRequestHandler> logger)
        {
            this.amenityRepository = amenityRepository;
            this.logger = logger;
        }

        public async Task Handle(AmenityRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling AmenityRemoveRequest for Amenity Id: {AmenityId}", request.Id);
            
            logger.LogInformation("Retrieving amenity with ID {Id}", request.Id);
            var entity = await amenityRepository.GetAsync(x => x.Id == request.Id, cancellationToken);
            logger.LogInformation("Amenity with Id: {Id} retrieved successfully", request.Id);

            amenityRepository.Remove(entity);
            logger.LogInformation("Amenity with Id: {AmenityId} removed from repository", request.Id);

            await amenityRepository.SaveAsync(cancellationToken);
            logger.LogInformation("Changes saved successfully for Amenity Id: {AmenityId}", request.Id);
        }
    }
}
