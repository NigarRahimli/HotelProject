using MediatR;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using System.Threading;
using System.Threading.Tasks;
using Property = Project.Domain.Models.Entities.Property;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand
{
    class PropertyEditRequestHandler : IRequestHandler<PropertyEditRequest, Property>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IKindRepository kindRepository;
        private readonly IDescriptionRepository descriptionRepository;
        private readonly ILogger<PropertyEditRequestHandler> logger;

        public PropertyEditRequestHandler(
            IPropertyRepository propertyRepository,
            ILocationRepository locationRepository,
            IKindRepository kindRepository,
            IDescriptionRepository descriptionRepository,
            ILogger<PropertyEditRequestHandler> logger
        )
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.kindRepository = kindRepository;
            this.descriptionRepository = descriptionRepository;
            this.logger = logger;
        }

        public async Task<Property> Handle(PropertyEditRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving property with ID {PropertyId}", request.Id);
            var property = await propertyRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            logger.LogInformation("Retrieving description with ID {DescriptionId}", request.DescriptionId);
            var description = await descriptionRepository.GetAsync(m => m.Id == request.DescriptionId, cancellationToken);

            logger.LogInformation("Retrieving kind with ID {KindId}", request.KindId);
            var kind = await kindRepository.GetAsync(m => m.Id == request.KindId, cancellationToken);

            logger.LogInformation("Retrieving location with ID {LocationId}", request.LocationId);
            var location = await locationRepository.GetAsync(m => m.Id == request.LocationId, cancellationToken);

            property.Name = request.Name;
            property.KindId = request.KindId;
            property.DescriptionId = request.DescriptionId;
            property.GuestNum = request.GuestNum;
            property.IsPetFriendly = request.IsPetFriendly;
            property.LocationId = request.LocationId;
            property.LongPrice = request.LongPrice;
            property.MedPrice = request.MedPrice;
            property.MinPrice = request.MinPrice;

            logger.LogInformation("Saving changes to property with ID {PropertyId}", property.Id);
            await propertyRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Property with ID {PropertyId} updated successfully", property.Id);
            return property;
        }
    }
}
