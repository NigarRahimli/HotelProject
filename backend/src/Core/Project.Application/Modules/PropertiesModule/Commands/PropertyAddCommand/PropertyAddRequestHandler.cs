using MediatR;
using NetTopologySuite.Geometries;
using Project.Application.Repositories;
using Project.Domain.Models.Entities;
using Project.Infrastructure.Exceptions;

namespace Project.Application.Modules.PropertiesModule.Commands.PropertyAddCommand
{
    class PropertyAddRequestHandler : IRequestHandler<PropertyAddRequest, Property>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IKindRepository kindRepository;
        private readonly IDescriptionRepository descriptionRepository;

        public PropertyAddRequestHandler(IPropertyRepository propertyRepository, ILocationRepository locationRepository, IKindRepository kindRepository, IDescriptionRepository descriptionRepository)
        {
            this.propertyRepository = propertyRepository;
            this.locationRepository = locationRepository;
            this.kindRepository = kindRepository;
            this.descriptionRepository = descriptionRepository;
        }
        public async Task<Property> Handle(PropertyAddRequest request, CancellationToken cancellationToken)
        {

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
            var location = new Domain.Models.Entities.Location
            {
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                Coordinates = new Point(request.Latitude, request.Longitude) { SRID = 4326 }
            };
            await locationRepository.AddAsync(location, cancellationToken);
            await locationRepository.SaveAsync(cancellationToken);

            var property = new Property
            {
                Name = request.Name,
                DescriptionId = request.DescriptionId,
                GuestNum=request.GuestNum,
                KindId= request.KindId,
                CreatedAt = DateTime.UtcNow,
                LocationId = location.Id,
                IsPetFriendly=request.IsPetFriendly,
                CreatedBy = 1
            };
            await propertyRepository.AddAsync(property, cancellationToken);
            await propertyRepository.SaveAsync(cancellationToken);

            return property;
        }
    }
}
