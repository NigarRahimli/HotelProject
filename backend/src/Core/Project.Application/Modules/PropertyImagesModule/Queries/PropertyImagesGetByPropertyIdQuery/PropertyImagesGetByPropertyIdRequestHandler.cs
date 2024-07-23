using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.PropertyImagesModule.Query.PropertyImagesGetByPropertyIdQuery
{
    public class PropertyImagesGetByPropertyIdRequestHandler : IRequestHandler<PropertyImagesGetByPropertyIdRequest, IEnumerable<PropertyImageDetailsDto>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PropertyImagesGetByPropertyIdRequestHandler> logger;

        public PropertyImagesGetByPropertyIdRequestHandler(
            IPropertyImageRepository propertyImageRepository,
            IMapper mapper,
            ILogger<PropertyImagesGetByPropertyIdRequestHandler> logger,
            IPropertyRepository propertyRepository)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<PropertyImageDetailsDto>> Handle(PropertyImagesGetByPropertyIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling PropertyImagesGetByPropertyIdRequest for PropertyId: {PropertyId}", request.PropertyId);
            var property = await propertyRepository.GetAsync(x => x.Id == request.PropertyId && x.DeletedBy == null, cancellationToken);

            logger.LogInformation("Checked property with PropertyId {PropertyId} exists", request.PropertyId);

            var entities = await propertyImageRepository
                .GetAll(m => m.DeletedBy == null && m.PropertyId == request.PropertyId)
                .ToListAsync(cancellationToken);

            logger.LogInformation("Retrieved {Count} property images for PropertyId: {PropertyId}", entities.Count, request.PropertyId);

            var dto = mapper.Map<IEnumerable<PropertyImageDetailsDto>>(entities);
            return dto;
        }
    }
}
