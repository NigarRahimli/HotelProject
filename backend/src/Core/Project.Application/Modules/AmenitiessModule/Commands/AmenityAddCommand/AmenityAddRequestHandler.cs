using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.AmenitiesModule.Commands.AmenityAddCommand
{
    class AmenityAddRequestHandler : IRequestHandler<AmenityAddRequest, Amenity>
    {
        private readonly IAmenityRepository AmenityRepository;

        public AmenityAddRequestHandler(IAmenityRepository AmenityRepository)
        {
            this.AmenityRepository = AmenityRepository;
        }
        public async Task<Amenity> Handle(AmenityAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Amenity
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await AmenityRepository.AddAsync(entity, cancellationToken);
            await AmenityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
