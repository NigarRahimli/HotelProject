using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityAddCommand
{
    class FacilityAddRequestHandler : IRequestHandler<FacilityAddRequest, Facility>
    {
        private readonly IFacilityRepository FacilityRepository;

        public FacilityAddRequestHandler(IFacilityRepository FacilityRepository)
        {
            this.FacilityRepository = FacilityRepository;
        }
        public async Task<Facility> Handle(FacilityAddRequest request, CancellationToken cancellationToken)
        {
            var entity = new Facility
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await FacilityRepository.AddAsync(entity, cancellationToken);
            await FacilityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
