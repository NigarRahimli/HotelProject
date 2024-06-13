using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilityCountsModule.Queries.FacilityCountGetAllQuery
{
    class FacilityCountGetAllRequestHandler : IRequestHandler<FacilityCountGetAllRequest, IEnumerable<FacilityCount>>
    {
        private readonly IFacilityCountRepository FacilityCountRepository;

        public FacilityCountGetAllRequestHandler(IFacilityCountRepository FacilityCountRepository)
        {
            this.FacilityCountRepository = FacilityCountRepository;
        }
        public async Task<IEnumerable<FacilityCount>> Handle(FacilityCountGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await FacilityCountRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
