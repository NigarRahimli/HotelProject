using MediatR;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.FacilityCountsModule.Commands.FacilityCountAddCommand
{
    class FacilityCountAddRequestHandler : IRequestHandler<FacilityCountAddRequest, FacilityCount>
    {
        private readonly IFacilityCountRepository facilityCountRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IFacilityRepository facilityRepository;

        public FacilityCountAddRequestHandler(IFacilityCountRepository facilityCountRepository/*,IPropertyRepository propertyRepository, IFacilityRepository facilityRepository*/)
        {
            this.facilityCountRepository = facilityCountRepository;
            //this.propertyRepository = propertyRepository;
            //this.facilityRepository = facilityRepository;
        }
        public async Task<FacilityCount> Handle(FacilityCountAddRequest request, CancellationToken cancellationToken)
        {
            //var property = await propertyRepository.GetAsync(x=>x.Id==request.PropertyId);
            //var facility = await facilityRepository.GetAsync(x => x.Id == request.FacilityId);

            //if (property == null || property.DeletedAt.HasValue)
            //{
            //    throw new Exception("The property does not exist or is deleted.");
            //}

            //if (facility == null || facility.DeletedAt.HasValue)
            //{
            //    throw new Exception("The facility does not exist or is deleted.");
            //}
            var entity = new FacilityCount
            {   PropertyId=request.PropertyId,
                FacilityId=request.FacilityId,
                Count = request.Count,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };
            await facilityCountRepository.AddAsync(entity, cancellationToken);
            await facilityCountRepository.SaveAsync(cancellationToken);

            return entity;
        }
    }
}
