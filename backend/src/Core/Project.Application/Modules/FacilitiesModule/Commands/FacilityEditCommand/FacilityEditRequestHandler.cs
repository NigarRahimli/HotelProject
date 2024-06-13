using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityEditCommand
{
    class FacilityEditRequestHandler : IRequestHandler<FacilityEditRequest, Facility>
    {
        private readonly IFacilityRepository FacilityRepository;

        public FacilityEditRequestHandler(IFacilityRepository FacilityRepository)
        {
            this.FacilityRepository = FacilityRepository;
        }
        public async Task<Facility> Handle(FacilityEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await FacilityRepository.GetAsync(m=>m.Id==request.Id);

            entity.Name=request.Name;
            await FacilityRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
