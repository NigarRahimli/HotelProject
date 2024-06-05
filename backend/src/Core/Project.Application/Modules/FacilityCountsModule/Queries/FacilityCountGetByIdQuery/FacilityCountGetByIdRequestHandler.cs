using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetByIdQuery
{
    class FacilityCountGetByIdRequestHandler : IRequestHandler<FacilityCountGetByIdRequest, FacilityCount>
    {
        private readonly IFacilityCountRepository FacilityCountRepository;

        public FacilityCountGetByIdRequestHandler(IFacilityCountRepository FacilityCountRepository)
        {
            this.FacilityCountRepository = FacilityCountRepository;
        }
        public async Task<FacilityCount> Handle(FacilityCountGetByIdRequest request, CancellationToken cancellationToken)
        {
            var entity=await FacilityCountRepository.GetAsync(x=>x.Id==request.Id&& x.DeletedBy==null,cancellationToken);
            return entity;
        }
    }
}
