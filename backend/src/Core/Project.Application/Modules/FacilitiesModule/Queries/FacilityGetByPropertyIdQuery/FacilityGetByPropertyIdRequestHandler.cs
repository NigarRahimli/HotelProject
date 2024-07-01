using MediatR;
using Project.Application.Modules.FacilitiesModule.Queries;
using Project.Application.Modules.FacilitiesModule.Queries.FacilityGetByPropertyIdQuery;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Exceptions;


namespace Project.Application.Modules.FacilitiesModule.Handlers
{
    public class FacilityGetByPropertyIdRequestHandler : IRequestHandler<FacilityGetByPropertyIdRequest, IEnumerable<FacilityDetailDto>>
    {
        private readonly IFacilityRepository facilityRepository;
        private readonly IPropertyRepository propertyRepository;

        public FacilityGetByPropertyIdRequestHandler(IFacilityRepository facilityRepository, IPropertyRepository propertyRepository)
        {
            this.facilityRepository = facilityRepository;
            this.propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<FacilityDetailDto>> Handle(FacilityGetByPropertyIdRequest request, CancellationToken cancellationToken)
        {
            var property = await propertyRepository.GetAsync(m => m.Id == request.PropertyId, cancellationToken);
            //if (property == null)
            //{
            //    throw new NotFoundException($"{nameof(Property)} with {request.PropertyId} not found");
            //}
            var facilityDtos = await facilityRepository.GetFacilitiesByPropertyIdAsync(request.PropertyId,cancellationToken);
            return facilityDtos;
        }
    }
}
