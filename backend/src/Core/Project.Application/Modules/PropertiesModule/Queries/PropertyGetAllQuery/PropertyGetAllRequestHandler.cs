using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Modules.PropertiesModule.Queries.PropertyGetAllQuery
{
    class PropertyGetAllRequestHandler : IRequestHandler<PropertyGetAllRequest, IEnumerable<Property>>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<PropertyGetAllRequestHandler> logger;

        public PropertyGetAllRequestHandler(IPropertyRepository propertyRepository, ILogger<PropertyGetAllRequestHandler> logger)
        {
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<Property>> Handle(PropertyGetAllRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving all properties");
            var entities = await propertyRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);

            if (entities == null || !entities.Any())
            {
                logger.LogWarning("No properties found");
            }

            return entities;
        }
    }
}
