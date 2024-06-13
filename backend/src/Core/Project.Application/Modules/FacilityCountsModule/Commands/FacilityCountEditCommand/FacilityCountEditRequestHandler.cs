using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountEditCommand
{
    class FacilityCountEditRequestHandler : IRequestHandler<FacilityCountEditRequest, FacilityCount>
    {
        private readonly IFacilityCountRepository FacilityCountRepository;

        public FacilityCountEditRequestHandler(IFacilityCountRepository FacilityCountRepository)
        {
            this.FacilityCountRepository = FacilityCountRepository;
        }
        public async Task<FacilityCount> Handle(FacilityCountEditRequest request, CancellationToken cancellationToken)
        {
            var entity=await FacilityCountRepository.GetAsync(m=>m.Id==request.Id);

            entity.Count = request.Count;
            await FacilityCountRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
