using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.AmenitiesModule.Queries.AmenityGetByIdQuery
{
    class AmenityGetByIdRequestHandler : IRequestHandler<AmenityGetByIdRequest, Amenity>
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly ILogger<AmenityGetByIdRequestHandler> logger;

        public AmenityGetByIdRequestHandler(IAmenityRepository amenityRepository, ILogger<AmenityGetByIdRequestHandler> logger)
        {
            this.amenityRepository = amenityRepository;
            this.logger = logger;
        }

        public async Task<Amenity> Handle(AmenityGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling AmenityGetByIdRequest for Amenity Id: {AmenityId}", request.Id);

            var entity = await amenityRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null, cancellationToken);
            if (entity == null)
            {
                logger.LogWarning("Amenity with Id: {AmenityId} not found or deleted", request.Id);
                throw new Exception($"Amenity with Id: {request.Id} not found or deleted");
            }

            logger.LogInformation("Successfully retrieved Amenity with Id: {AmenityId}", request.Id);
            return entity;
        }
    }
}
