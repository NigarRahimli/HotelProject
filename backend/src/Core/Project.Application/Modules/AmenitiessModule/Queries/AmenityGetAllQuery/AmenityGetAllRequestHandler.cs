using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.AmenitiesModule.Queries.AmenityGetAllQuery
{
    class AmenityGetAllRequestHandler : IRequestHandler<AmenityGetAllRequest, IEnumerable<Amenity>>
    {
        private readonly IAmenityRepository AmenityRepository;

        public AmenityGetAllRequestHandler(IAmenityRepository AmenityRepository)
        {
            this.AmenityRepository = AmenityRepository;
        }
        public async Task<IEnumerable<Amenity>> Handle(AmenityGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await AmenityRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
