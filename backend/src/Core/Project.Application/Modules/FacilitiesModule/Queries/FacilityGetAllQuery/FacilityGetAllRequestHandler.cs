using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilitiesModule.Queries.FacilityGetAllQuery
{
    class FacilityGetAllRequestHandler : IRequestHandler<FacilityGetAllRequest, IEnumerable<Facility>>
    {
        private readonly IFacilityRepository FacilityRepository;

        public FacilityGetAllRequestHandler(IFacilityRepository FacilityRepository)
        {
            this.FacilityRepository = FacilityRepository;
        }
        public async Task<IEnumerable<Facility>> Handle(FacilityGetAllRequest request, CancellationToken cancellationToken)
        {
            var entities = await FacilityRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            return entities;
        }
    }
}
