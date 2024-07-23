using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;

namespace Project.Application.Modules.PropertyImagesModule.Queries.PropertyImageGetAllQuery
{
    public class PropertyImageGetAllRequestHandler : IRequestHandler<PropertyImageGetAllRequest, IEnumerable<PropertyImage>>
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly ILogger<PropertyImageGetAllRequestHandler> logger;

        public PropertyImageGetAllRequestHandler(
            IPropertyImageRepository propertyImageRepository,
            ILogger<PropertyImageGetAllRequestHandler> logger)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<PropertyImage>> Handle(PropertyImageGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting to handle PropertyImageGetAllRequest");

            var entities = await propertyImageRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);

            logger.LogInformation("Retrieved {Count} property images", entities.Count);

            return entities;
        }
    }
}
