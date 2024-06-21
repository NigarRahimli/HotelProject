using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetTopologySuite.Geometries;
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
        private readonly GeometryFactory geometryFactory;

        public PropertyEditRequestHandler(
            IPropertyRepository propertyRepository,
            ILocationRepository locationRepository,
            IKindRepository kindRepository,
            IDescriptionRepository descriptionRepository,
            GeometryFactory geometryFactory)
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.kindRepository = kindRepository;
            this.descriptionRepository = descriptionRepository;
            this.geometryFactory = geometryFactory;
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

            Location location;
            if (request.LocationId > 0)
            {
                location = await locationRepository.GetAsync(m => m.Id == request.LocationId, cancellationToken);
                if (location == null)
                {
                    throw new NotFoundException($"{nameof(Location)} with {request.LocationId} not found");
                }
            }
            else
            {
                if (request.Longitude == null || request.Latitude == null)
                {
                    throw new Exception("Longitude and Latitude cannot be null");
                }

                location = new Location
                {
                    Latitude = (double)request.Latitude,
                    Longitude=(double)request.Longitude,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    Country = request.Country,
                    ZipCode = request.ZipCode
                };

                await locationRepository.AddAsync(location, cancellationToken);
                await locationRepository.SaveAsync(cancellationToken);
            }

            property.Name = request.Name;
            property.KindId = request.KindId;
            property.DescriptionId = request.DescriptionId;
            property.GuestNum = request.GuestNum;
            property.IsPetFriendly = request.IsPetFriendly;
            property.LocationId = location.Id;

            await propertyRepository.SaveAsync(cancellationToken);

            return property;
        }
    }
}
