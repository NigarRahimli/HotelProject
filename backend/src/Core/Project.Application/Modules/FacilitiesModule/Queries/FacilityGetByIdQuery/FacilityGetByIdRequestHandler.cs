using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByIdQuery
{
    class FacilityGetByIdRequestHandler : IRequestHandler<FacilityGetByIdRequest, Facility>
    {
        private readonly IFacilityRepository FacilityRepository;

        public FacilityGetByIdRequestHandler(IFacilityRepository FacilityRepository)
        {
            this.FacilityRepository = FacilityRepository;
        }
        public async Task<Facility> Handle(FacilityGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await FacilityRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
