using MediatR;
using Project.Application.Repositories;


namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountRemoveCommand
{
    class FacilityCountRemoveRequestHandler : IRequestHandler<FacilityCountRemoveRequest>
    {
        private readonly IFacilityCountRepository FacilityCountRepository;

        public FacilityCountRemoveRequestHandler(IFacilityCountRepository FacilityCountRepository)
        {
            this.FacilityCountRepository = FacilityCountRepository;
        }
        public async Task Handle(FacilityCountRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity =await FacilityCountRepository.GetAsync(x=>x.Id==request.Id,cancellationToken);
            FacilityCountRepository.Remove(entity);
            await FacilityCountRepository.SaveAsync(cancellationToken);
        }
    }
}
