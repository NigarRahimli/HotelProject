using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyRemoveCommand
{
    class PropertyRemoveRequestHandler : IRequestHandler<PropertyRemoveRequest>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILogger<PropertyRemoveRequestHandler> logger;

        public PropertyRemoveRequestHandler(IPropertyRepository propertyRepository, ILogger<PropertyRemoveRequestHandler> logger)
        {
            this.propertyRepository = propertyRepository;
            this.logger = logger;
        }

        public async Task Handle(PropertyRemoveRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving property with ID {PropertyId}", request.Id);
            var entity = await propertyRepository.GetAsync(x => x.Id == request.Id && x.DeletedBy==null, cancellationToken);
            logger.LogInformation("Property with Id: {Id} retrieved successfully", request.Id);


            logger.LogInformation("Removing property with ID {PropertyId}", request.Id);
            propertyRepository.Remove(entity);

            logger.LogInformation("Saving changes after removing property with ID {PropertyId}", request.Id);
            await propertyRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Property with ID {PropertyId} removed successfully", request.Id);
        }
    }
}
