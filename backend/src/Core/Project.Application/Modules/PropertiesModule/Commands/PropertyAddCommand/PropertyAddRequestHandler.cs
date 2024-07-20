using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Extensions;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyAddCommand
{
    class PropertyAddRequestHandler : IRequestHandler<PropertyAddRequest, Property>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IKindRepository kindRepository;
        private readonly IDescriptionRepository descriptionRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<PropertyAddRequestHandler> logger;

        public PropertyAddRequestHandler(
            IPropertyRepository propertyRepository,
            ILocationRepository locationRepository,
            IKindRepository kindRepository,
            IDescriptionRepository descriptionRepository,
            IHttpContextAccessor httpContextAccessor,
            ILogger<PropertyAddRequestHandler> logger)
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.kindRepository = kindRepository;
            this.descriptionRepository = descriptionRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<Property> Handle(PropertyAddRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling PropertyAddRequest");

            logger.LogInformation("Retrieving description with ID {DescriptionId}", request.DescriptionId);
            var description = await descriptionRepository.GetAsync(m => m.Id == request.DescriptionId, cancellationToken);

            logger.LogInformation("Retrieving kind with ID {KindId}", request.KindId);
            var kind = await kindRepository.GetAsync(m => m.Id == request.KindId, cancellationToken);

            logger.LogInformation("Retrieving location with ID {LocationId}", request.LocationId);
            var location = await locationRepository.GetAsync(m => m.Id == request.LocationId, cancellationToken);

            var userId = httpContextAccessor.HttpContext.GetUserIdExtension();
            logger.LogInformation("Adding property for user ID {UserId}", userId);

            var property = new Property
            {
                Name = request.Name,
                DescriptionId = request.DescriptionId,
                GuestNum = request.GuestNum,
                KindId = request.KindId,
                LocationId = request.LocationId,
                IsPetFriendly = request.IsPetFriendly,
                LongPrice = request.LongPrice,
                MedPrice = request.MedPrice,
                MinPrice = request.MinPrice,
            };

            await propertyRepository.AddAsync(property, cancellationToken);
            await propertyRepository.SaveAsync(cancellationToken);

            logger.LogInformation("Property with ID {PropertyId} added successfully", property.Id);

            return property;
        }
    }
}
