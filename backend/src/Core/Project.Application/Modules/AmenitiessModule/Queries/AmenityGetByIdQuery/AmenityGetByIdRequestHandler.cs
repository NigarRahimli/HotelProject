using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.AmenitiesModule.Queries.AmenityGetByIdQuery
{
    class AmenityGetByIdRequestHandler : IRequestHandler<AmenityGetByIdRequest, Amenity>
    {
        private readonly IAmenityRepository AmenityRepository;

        public AmenityGetByIdRequestHandler(IAmenityRepository AmenityRepository)
        {
            this.AmenityRepository = AmenityRepository;
        }
        public async Task<Amenity> Handle(AmenityGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await AmenityRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
