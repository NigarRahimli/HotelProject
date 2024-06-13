using MediatR;
using Project.Application.Repositories;


namespace Project.Application.Modules.FacilitiesModule.Commands.FacilityRemoveCommand
{
    class FacilityRemoveRequestHandler : IRequestHandler<FacilityRemoveRequest>
    {
        private readonly IFacilityRepository FacilityRepository;

        public FacilityRemoveRequestHandler(IFacilityRepository FacilityRepository)
        {
            this.FacilityRepository = FacilityRepository;
        }
        public async Task Handle(FacilityRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await FacilityRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            FacilityRepository.Remove(entity);
            await FacilityRepository.SaveAsync(cancellationToken);
        }
    }
}
