using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Exceptions;
using Location = Project.Domain.Models.Entities.Location;
using Property = Project.Domain.Models.Entities.Property;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyEditCommand
{
    class PropertyEditRequestHandler : IRequestHandler<PropertyEditRequest, Property>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IKindRepository kindRepository;
        private readonly IDescriptionRepository descriptionRepository;


        public PropertyEditRequestHandler(
            IPropertyRepository propertyRepository,
            ILocationRepository locationRepository,
            IKindRepository kindRepository,
            IDescriptionRepository descriptionRepository
         )
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.kindRepository = kindRepository;
            this.descriptionRepository = descriptionRepository;
            
        }

        public async Task<Property> Handle(PropertyEditRequest request, CancellationToken cancellationToken)
        {
            var property = await propertyRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
            if (property == null)
            {
                throw new NotFoundException($"{nameof(Property)} with {request.Id} not found");
            }

            var description = await descriptionRepository.GetAsync(m => m.Id == request.DescriptionId, cancellationToken);
            if (description == null)
            {
                throw new NotFoundException($"{nameof(Description)} with {request.DescriptionId} not found");
            }

            var kind = await kindRepository.GetAsync(m => m.Id == request.KindId, cancellationToken);
            if (kind == null)
            {
                throw new NotFoundException($"{nameof(Kind)} with {request.KindId} not found");
            }
            var location = await locationRepository.GetAsync(m => m.Id == request.LocationId, cancellationToken);
            if (kind == null)
            {
                throw new NotFoundException($"{nameof(Location)} with {request.LocationId} not found");
            }


            property.Name = request.Name;
            property.KindId = request.KindId;
            property.DescriptionId = request.DescriptionId;
            property.GuestNum = request.GuestNum;
            property.IsPetFriendly = request.IsPetFriendly;
            property.LocationId = request.LocationId;
            property.LongPrice = request.LongPrice;
            property.MedPrice = request.MedPrice;
            property.MinPrice= request.MinPrice;

            await propertyRepository.SaveAsync(cancellationToken);

            return property;
        }
    }
}
