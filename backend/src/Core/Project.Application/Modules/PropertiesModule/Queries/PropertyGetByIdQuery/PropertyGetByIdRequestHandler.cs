using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetByIdQuery
{
    public class PropertyGetByIdRequestHandler : IRequestHandler<PropertyGetByIdRequest, Property>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<PropertyGetByIdRequestHandler> logger;

        public PropertyGetByIdRequestHandler(
            IPropertyRepository propertyRepository,
            ILogger<PropertyGetByIdRequestHandler> logger)
        {
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task<Property> Handle(PropertyGetByIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving property with ID {PropertyId}", request.Id);

            var entity = await propertyRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy == null,cancellationToken);

            
            logger.LogWarning("Property with ID {PropertyId} successfully retrieved", request.Id);
          

            return entity;
        }
    }
}
